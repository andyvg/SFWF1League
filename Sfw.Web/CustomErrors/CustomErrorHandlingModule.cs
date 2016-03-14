using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Sfw.Web.CustomErrors
{
    /// <summary>
    /// HttpModule to handle the customErrors section of the web.config file to allow using
    /// .cshtml files to handle error pages. The module needs registering in the web.config
    /// or in code using WebActivator.
    /// </summary>
    /// <remarks>
    /// Adapted from https://github.com/PureKrome/MagicalUnicorn.MvcErrorToolkit
    /// </remarks>
    public sealed class CustomErrorHandlingModule : IHttpModule
    {
        private static CustomErrorsSection _customErrorsSection;

        #region Implementation of IHttpModule

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="httpApplication">An <see cref="T:System.Web.HttpApplication"/> 
        /// that provides access to the methods, properties, and events common to all 
        /// application objects within an ASP.NET application </param>
        public void Init(HttpApplication httpApplication)
        {
            // :: IMPORTANT EVENTS NOTE ::
            // The EndRequest event is fired every time, even if the Error event is also fired.
            // This means we don't want to try and handle custom errors, twice.
            // So we'll leverage the context's ITEMS property to communicate between these
            // two events.

            // Make this key unique
            string hasHandledAnErrorKey = "HasHandledAnError_" + DateTime.Now.Ticks;

            httpApplication.EndRequest += (sender, e) =>
            {
                // Check to make sure the Http status is NOT 200.
                if (!httpApplication.Context.Items.Contains(hasHandledAnErrorKey) &&
                    httpApplication.Response.StatusCode != (int)HttpStatusCode.OK)
                {
                    HandleCustomErrors(httpApplication,
                                       (HttpStatusCode)
                                       httpApplication.Response.StatusCode);
                }
            };

            httpApplication.Error += (sender, e) =>
            {
                // Handle the error.
                HandleCustomErrors(httpApplication);

                // This request has already handled the error.
                // This is so the EndRequest event doesn't try to handle the custom
                // errors a 2nd time!
                httpApplication.Context.Items.Add(hasHandledAnErrorKey, true);
            };
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements 
        /// <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            // No Op.
        }

        #endregion

        private static CustomErrorsSection CustomErrorsSection
        {
            get
            {
                if (_customErrorsSection != null)
                {
                    return _customErrorsSection;
                }

                Configuration configuration = WebConfigurationManager.OpenWebConfiguration("/");
                return _customErrorsSection = configuration.GetSection("system.web/customErrors") as CustomErrorsSection;
            }
        }

        private static void HandleCustomErrors(HttpApplication httpApplication,
                                               HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            // Do not show the custom errors if
            // a) CustomErrors mode == "off" or not set.
            // b) Mode == RemoteOnly and we are on our local development machine.
            if (!httpApplication.Context.IsCustomErrorEnabled ||
                (CustomErrorsSection.Mode == CustomErrorsMode.RemoteOnly && httpApplication.Request.IsLocal))
            {
                return;
            }

            // Remember the current error.
            Exception currentError = HttpContext.Current.Error;

            // Check for an HttpErrorException. Eg. 404 Not found or 401 Not Authorised
            var httpErrorException = currentError as HttpException;
            if (httpErrorException != null)
            {
                httpStatusCode = (HttpStatusCode)httpErrorException.GetHttpCode();
            }

            // Render the view, be it html or json, etc.
            RenderErrorView(httpApplication, httpStatusCode, currentError);

            // Clear all the errors otherwise it shows the error page.
            HttpContext.Current.ClearError();

            // Avoid any IIS low level errors.
            httpApplication.Response.TrySkipIisCustomErrors = true;
        }

        private static void RenderErrorView(HttpApplication httpApplication,
            HttpStatusCode httpStatusCode, Exception currentError)
        {
            // Is this an AJAX request?
            if (httpApplication.Context.Request.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                RenderAjaxView(httpApplication, httpStatusCode, currentError);
            }
            else
            {
                RenderNormalView(httpApplication, httpStatusCode, currentError);
            }
        }

        private static void RenderAjaxView(HttpApplication httpApplication, HttpStatusCode httpStatusCode,
                                             Exception currentError)
        {
            // Check if this content type contains a request for json.
            string errorMessage = httpApplication.Request.ContentType.Contains("json")
                                       ? currentError.Message
                                       : string.Format(
                                           "An error occured but we are unable to handle the request.ContentType [{0}]. The error is: {1}",
                                           httpApplication.Request.ContentType,
                                           currentError.Message);


            var errorController = new FakeErrorController();
            var controllerContext =
                new ControllerContext(httpApplication.Context.Request.RequestContext, errorController);
            var jsonResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    error_message = errorMessage
                }
            };
            jsonResult.ExecuteResult(controllerContext);

            // Set the correct Error Status code
            httpApplication.Response.StatusCode = (int)httpStatusCode;
        }

        private static void RenderNormalView(HttpApplication httpApplication, HttpStatusCode httpStatusCode,
                                             Exception currentError)
        {
            // Get the required view
            string viewPath = GetCustomErrorRedirect(httpStatusCode);
            if (string.IsNullOrEmpty(viewPath))
            {
                // Either
                // 1. No customErrors was provided
                // 2. No redirect was provided for the httpStatus code.
                throw new InvalidOperationException("No view path was determined.");
            }

            // Render the view provided or fallback if a view isn't provided
            if (string.IsNullOrEmpty(viewPath))
            {
                RenderFallBackErrorViewBecauseNoneWasProvided(httpApplication, httpStatusCode, currentError);
            }
            else
            {
                RenderCustomErrorView(httpApplication, viewPath, httpStatusCode,
                                      currentError);
            }
        }

        private static string GetCustomErrorRedirect(HttpStatusCode httpStatusCode)
        {
            if (CustomErrorsSection == null)
            {
                return null;
            }

            string redirect = null;
            if (CustomErrorsSection.Errors != null)
            {
                CustomError customError = CustomErrorsSection.Errors[((int)httpStatusCode).ToString()];
                if (customError != null)
                {
                    redirect = customError.Redirect;
                }
            }

            // Return the redirect if determined, otherwise use the default redirect.
            return string.IsNullOrEmpty(redirect) ? CustomErrorsSection.DefaultRedirect : redirect;
        }

        private static void RenderCustomErrorView(HttpApplication httpApplication, string viewPath,
                                                  HttpStatusCode httpStatusCode,
                                                  Exception currentError)
        {
            try
            {
                // Render the view which requires a viewContext and a Controller.
                // Instantiate a fake controller which does nothing and then work 
                // towards rendering the view.
                var errorController = new FakeErrorController();
                var controllerContext =
                    new ControllerContext(httpApplication.Context.Request.RequestContext, errorController);
                var view = new RazorView(controllerContext, viewPath, null, false, null);
                var viewModel = new ErrorViewModel
                {
                    Exception = currentError
                };
                var tempData = new TempDataDictionary();
                var viewContext = new ViewContext(controllerContext, view,
                                                  new ViewDataDictionary(viewModel), tempData,
                                                  httpApplication.Response.Output);
                view.Render(viewContext, httpApplication.Response.Output);

                // Set the correct Error Status code
                httpApplication.Response.StatusCode = (int)httpStatusCode;
            }
            catch (Exception exception)
            {
                // Error eg. the path to the redirect might not exist, etc.
                string errorMessage =
                    string.Format(
                        "An error occured while trying to Render the custom error view which you provided, for this HttpStatusCode. ViewPath: {0}; Message: {1}",
                        string.IsNullOrEmpty(viewPath) ? "--no view path was provided!! --" : viewPath,
                        exception.Message);

                RenderFallBackErrorViewBecauseNoneWasProvided(httpApplication,
                                                              HttpStatusCode.InternalServerError,
                                                              new InvalidOperationException(errorMessage, currentError));
            }
        }

        private static void RenderFallBackErrorViewBecauseNoneWasProvided(HttpApplication httpApplication,
                                                                          HttpStatusCode httpStatusCode,
                                                                          Exception currentError)
        {
            const string simpleErrorMessage =
                @"<html>
                    <head>
                        <title>Error</title>
                    </head>
                    <body>
                        <h3>Sorry, an error occurred while processing your request.</h3>
                    </body>
                  </html>";
            httpApplication.Response.Output.Write(simpleErrorMessage);

            // Set the correct Error Status code
            httpApplication.Response.StatusCode = (int)httpStatusCode;
        }
    }
}
