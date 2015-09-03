using System.Web.Optimization;

namespace Project.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.js",
                "~/bower_components/jquery-validation/dist/jquery.validate.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js",
                "~/js/site.js"));

            
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/bower_components/bootstrap/dist/css/bootstrap.css", "~/css/site.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}