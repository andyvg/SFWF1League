﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class Mvc
{
    public static Sfw.Racing.Web.Controllers.AccountController Account = new Sfw.Racing.Web.Controllers.T4MVC_AccountController();
    public static Sfw.Racing.Web.Controllers.Base.AuthorizeController Authorize = new Sfw.Racing.Web.Controllers.Base.T4MVC_AuthorizeController();
    public static Sfw.Racing.Web.Controllers.ConstructorController Constructor = new Sfw.Racing.Web.Controllers.T4MVC_ConstructorController();
    public static Sfw.Racing.Web.Controllers.DriverController Driver = new Sfw.Racing.Web.Controllers.T4MVC_DriverController();
    public static Sfw.Racing.Web.Controllers.EngineController Engine = new Sfw.Racing.Web.Controllers.T4MVC_EngineController();
    public static Sfw.Racing.Web.Controllers.HomeController Home = new Sfw.Racing.Web.Controllers.T4MVC_HomeController();
    public static Sfw.Racing.Web.Controllers.ManageController Manage = new Sfw.Racing.Web.Controllers.T4MVC_ManageController();
    public static Sfw.Racing.Web.Controllers.PlayerController Player = new Sfw.Racing.Web.Controllers.T4MVC_PlayerController();
    public static Sfw.Racing.Web.Controllers.RaceController Race = new Sfw.Racing.Web.Controllers.T4MVC_RaceController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string _references_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/_references.min.js") ? Url("_references.min.js") : Url("_references.js");
        public static readonly string bootstrap_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.js") ? Url("bootstrap.min.js") : Url("bootstrap.js");
        public static readonly string bootstrap_min_js = Url("bootstrap.min.js");
        public static readonly string jquery_1_10_2_intellisense_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.10.2.intellisense.min.js") ? Url("jquery-1.10.2.intellisense.min.js") : Url("jquery-1.10.2.intellisense.js");
        public static readonly string jquery_1_10_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-1.10.2.min.js") ? Url("jquery-1.10.2.min.js") : Url("jquery-1.10.2.js");
        public static readonly string jquery_1_10_2_min_js = Url("jquery-1.10.2.min.js");
        public static readonly string jquery_1_10_2_min_map = Url("jquery-1.10.2.min.map");
        public static readonly string jquery_tablesorter_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.tablesorter.min.js") ? Url("jquery.tablesorter.min.js") : Url("jquery.tablesorter.js");
        public static readonly string jquery_tablesorter_min_js = Url("jquery.tablesorter.min.js");
        public static readonly string jquery_tablesorter_pager_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.tablesorter.pager.min.js") ? Url("jquery.tablesorter.pager.min.js") : Url("jquery.tablesorter.pager.js");
        public static readonly string jquery_validate_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate-vsdoc.min.js") ? Url("jquery.validate-vsdoc.min.js") : Url("jquery.validate-vsdoc.js");
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.min.js") ? Url("jquery.validate.min.js") : Url("jquery.validate.js");
        public static readonly string jquery_validate_min_js = Url("jquery.validate.min.js");
        public static readonly string jquery_validate_unobtrusive_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.unobtrusive.min.js") ? Url("jquery.validate.unobtrusive.min.js") : Url("jquery.validate.unobtrusive.js");
        public static readonly string jquery_validate_unobtrusive_min_js = Url("jquery.validate.unobtrusive.min.js");
        public static readonly string modernizr_2_6_2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/modernizr-2.6.2.min.js") ? Url("modernizr-2.6.2.min.js") : Url("modernizr-2.6.2.js");
        public static readonly string respond_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/respond.min.js") ? Url("respond.min.js") : Url("respond.js");
        public static readonly string respond_min_js = Url("respond.min.js");
        public static readonly string Site_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Site.min.js") ? Url("Site.min.js") : Url("Site.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const string URLPATH = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.css") ? Url("bootstrap.min.css") : Url("bootstrap.css");
        public static readonly string bootstrap_min_css = Url("bootstrap.min.css");
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class constructors {
            private const string URLPATH = "~/Content/constructors";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string ferrari_png = Url("ferrari.png");
            public static readonly string forceindia_png = Url("forceindia.png");
            public static readonly string haas_png = Url("haas.png");
            public static readonly string manor_png = Url("manor.png");
            public static readonly string mclaren_png = Url("mclaren.png");
            public static readonly string mercedes_png = Url("mercedes.png");
            public static readonly string redbull_png = Url("redbull.png");
            public static readonly string renault_png = Url("renault.png");
            public static readonly string sauber_png = Url("sauber.png");
            public static readonly string tororosso_png = Url("tororosso.png");
            public static readonly string unknown_png = Url("unknown.png");
            public static readonly string williams_png = Url("williams.png");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class drivers {
            private const string URLPATH = "~/Content/drivers";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string alonso_jpg = Url("alonso.jpg");
            public static readonly string bottas_jpg = Url("bottas.jpg");
            public static readonly string button_jpg = Url("button.jpg");
            public static readonly string ericsson_jpg = Url("ericsson.jpg");
            public static readonly string grojean_jpg = Url("grojean.jpg");
            public static readonly string gutierrez_jpg = Url("gutierrez.jpg");
            public static readonly string hamilton_jpg = Url("hamilton.jpg");
            public static readonly string haryanto_jpg = Url("haryanto.jpg");
            public static readonly string hulkenberg_jpg = Url("hulkenberg.jpg");
            public static readonly string kvyat_jpg = Url("kvyat.jpg");
            public static readonly string magnussen_jpg = Url("magnussen.jpg");
            public static readonly string massa_jpg = Url("massa.jpg");
            public static readonly string nasr_jpg = Url("nasr.jpg");
            public static readonly string palmer_jpg = Url("palmer.jpg");
            public static readonly string perez_jpg = Url("perez.jpg");
            public static readonly string raikkonen_jpg = Url("raikkonen.jpg");
            public static readonly string ricciardo_jpg = Url("ricciardo.jpg");
            public static readonly string rosberg_jpg = Url("rosberg.jpg");
            public static readonly string sainz_jpg = Url("sainz.jpg");
            public static readonly string unknown_png = Url("unknown.png");
            public static readonly string verstappen_jpg = Url("verstappen.jpg");
            public static readonly string vettel_jpg = Url("vettel.jpg");
            public static readonly string wehrlein_jpg = Url("wehrlein.jpg");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class engines {
            private const string URLPATH = "~/Content/engines";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string ferrari_png = Url("ferrari.png");
            public static readonly string honda_png = Url("honda.png");
            public static readonly string mercedes_png = Url("mercedes.png");
            public static readonly string renault_png = Url("renault.png");
            public static readonly string unknown_png = Url("unknown.png");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Images {
            private const string URLPATH = "~/Content/Images";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string background_jpg = Url("background.jpg");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class questions {
            private const string URLPATH = "~/Content/questions";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string aus_question1_jpg = Url("aus_question1.jpg");
            public static readonly string aus_question2_jpg = Url("aus_question2.jpg");
            public static readonly string aus_question3_png = Url("aus_question3.png");
            public static readonly string bah_question1_png = Url("bah_question1.png");
            public static readonly string bah_question2_png = Url("bah_question2.png");
            public static readonly string bah_question3_png = Url("bah_question3.png");
        }
    
        public static readonly string Site_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Site.min.css") ? Url("Site.min.css") : Url("Site.css");
    }

    
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        public static partial class Scripts 
        {
            public static class Assets
            {
                public const string _references_js = "~/Scripts/_references.js"; 
                public const string bootstrap_js = "~/Scripts/bootstrap.js"; 
                public const string bootstrap_min_js = "~/Scripts/bootstrap.min.js"; 
                public const string jquery_1_10_2_intellisense_js = "~/Scripts/jquery-1.10.2.intellisense.js"; 
                public const string jquery_1_10_2_js = "~/Scripts/jquery-1.10.2.js"; 
                public const string jquery_1_10_2_min_js = "~/Scripts/jquery-1.10.2.min.js"; 
                public const string jquery_tablesorter_js = "~/Scripts/jquery.tablesorter.js"; 
                public const string jquery_tablesorter_min_js = "~/Scripts/jquery.tablesorter.min.js"; 
                public const string jquery_tablesorter_pager_js = "~/Scripts/jquery.tablesorter.pager.js"; 
                public const string jquery_validate_js = "~/Scripts/jquery.validate.js"; 
                public const string jquery_validate_min_js = "~/Scripts/jquery.validate.min.js"; 
                public const string jquery_validate_unobtrusive_js = "~/Scripts/jquery.validate.unobtrusive.js"; 
                public const string jquery_validate_unobtrusive_min_js = "~/Scripts/jquery.validate.unobtrusive.min.js"; 
                public const string modernizr_2_6_2_js = "~/Scripts/modernizr-2.6.2.js"; 
                public const string respond_js = "~/Scripts/respond.js"; 
                public const string respond_min_js = "~/Scripts/respond.min.js"; 
                public const string Site_js = "~/Scripts/Site.js"; 
            }
        }
        public static partial class Content 
        {
            public static partial class constructors 
            {
                public static class Assets
                {
                }
            }
            public static partial class drivers 
            {
                public static class Assets
                {
                }
            }
            public static partial class engines 
            {
                public static class Assets
                {
                }
            }
            public static partial class Images 
            {
                public static class Assets
                {
                }
            }
            public static partial class questions 
            {
                public static class Assets
                {
                }
            }
            public static class Assets
            {
                public const string bootstrap_css = "~/Content/bootstrap.css";
                public const string bootstrap_min_css = "~/Content/bootstrap.min.css";
                public const string Site_css = "~/Content/Site.css";
            }
        }
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114


