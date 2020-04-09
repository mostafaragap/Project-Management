using System.Web;
using System.Web.Optimization;

namespace ProjectManag
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
              "~/admin-lte/js/app.js",
              "~/admin-lte/js/adminlte.js",
              "~/admin-lte/plugins/fastclick/fastclick.js",
              "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/fullcalendar/fullcalendar.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/fullcalendar.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/admin-lte/css/skins/skin-blue.css",
                      "~/admin-lte/css/ionicons.min.css",
                      "~/content/font/font-awesome.min.css",
                      "~/content/font-awesome.min.css",
                      "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
                      ));
        }
    }
}
