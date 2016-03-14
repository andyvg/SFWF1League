using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sfw.Web.Attributes.Extensions
{
    public static class Extensions
    {
        private const string AutoCompleteControllerKey = "AutoCompleteController";
        private const string AutoCompleteActionKey = "AutoCompleteAction";
        private const string AutoCompleteCustomId = "AutoCompleteCustomId";
        public static void SetAutoComplete(this ModelMetadata metadata, string controller, string action, string customId)
        {
            metadata.AdditionalValues[AutoCompleteControllerKey] = controller;
            metadata.AdditionalValues[AutoCompleteActionKey] = action;
            metadata.AdditionalValues[AutoCompleteCustomId] = customId;
        }

        public static string GetAutoCompleteUrl(this HtmlHelper html, ModelMetadata metadata)
        {
            string controller = metadata.AdditionalValues.GetString(AutoCompleteControllerKey);
            string action = metadata.AdditionalValues.GetString(AutoCompleteActionKey);

            if (string.IsNullOrEmpty(controller)
                || string.IsNullOrEmpty(action))
            {
                return null;
            }


            return UrlHelper.GenerateUrl(null, action, controller, null, html.RouteCollection, html.ViewContext.RequestContext, true);
        }

        public static bool HasCustomId(this HtmlHelper html, ModelMetadata metadata)
        {
            return !string.IsNullOrEmpty(metadata.AdditionalValues.GetString(AutoCompleteCustomId));
        }

        public static string GetCustomId(this HtmlHelper html, ModelMetadata metadata)
        {
            return metadata.AdditionalValues.GetString(AutoCompleteCustomId);
        }

        private static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            object value;
            dictionary.TryGetValue(key, out value);
            return (string)value;
        }
    }
}