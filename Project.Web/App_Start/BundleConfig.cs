using System.Web.Optimization;

namespace Project.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bootstrap/js")
                .Include(
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.js", 
                "~/js/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include(
                "~/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/bower_components/jquery-validation/dist/jquery.validate.unobtrusive.js"));

            bundles.Add(new StyleBundle("~/bootstrap/css")
                .Include("~/bower_components/bootstrap/dist/css/bootstrap.css", "~/css/site.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}