using System.Web.Optimization;

namespace Tcbcsl.Presentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/script-base").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/moment.js",
                "~/Scripts/site.js",
                "~/Scripts/stats.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/DataTables/css/dataTables.bootstrap.css",
                "~/Content/Site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
