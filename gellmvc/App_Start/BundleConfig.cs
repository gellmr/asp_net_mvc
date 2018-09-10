using System.Web;
using System.Web.Optimization;

namespace gellmvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css_bundles/css").Include(
                      "~/dist/site.css"));

            bundles.Add(new ScriptBundle("~/js_bundles/jquery").Include(
                        "~/dist/jquery/jquery.js"));
                        
            bundles.Add(new ScriptBundle("~/js_bundles/bootstrap-sass").Include(
                        "~/dist/bootstrap-sass/bootstrap.js"));

            // validation is not used on every page, so its bundled separately to reduce page load time.
            bundles.Add(new ScriptBundle("~/js_bundles/jquery.validate").Include(
                        "~/dist/jquery.validation/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/js_bundles/Microsoft.jQuery.Unobtrusive.Validation").Include(
                        "~/dist/Microsoft.jQuery.Unobtrusive.Validation/jquery.validate.unobtrusive.js"));
      
            // allow debug mode to serve minified files.
            //BundleTable.EnableOptimizations = true;
        }
    }
}
