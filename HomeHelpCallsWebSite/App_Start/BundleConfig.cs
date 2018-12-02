using System.Web;
using System.Web.Optimization;

namespace HomeHelpCallsWebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",
                        "~/Scripts/bootstrap-rtl.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        //"~/Scripts/namespaces.js",
                        "~/Scripts/site.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      "~/Content/site.css",
                       "~/Content/bootstrap-rtl.min.css" ,
                       "~/Content/themes/base/jquery.ui.all.css"                      
                      ));

            #region LineParts
            bundles.Add(new ScriptBundle("~/bundles/PartsLines/parts").Include(
                      "~/Scripts/PartsLines/parts.js"));

          
            bundles.Add(new StyleBundle("~/Content/PartsLines/parts").Include(
                      "~/Content/PartsLines/parts.css"));
            #endregion LineParts


        }
    }
}
