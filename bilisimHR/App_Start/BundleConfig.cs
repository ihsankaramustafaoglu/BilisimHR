using System.Web;
using System.Web.Optimization;

namespace bilisimHR
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/global/plugins/jquery.min.js",
                        "~/assets/global/plugins/js.cookie.min.js",
                        "~/assets/global/plugins/jquery.blockui.min.js"));

            //bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/global/plugins/bootstrap/js/bootstrap.min.js"));
            
            //jquery validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                        "~/assets/global/plugins/jquery-validation/js/additional-methods.min.js"));
            
            //plugins
            bundles.Add(new ScriptBundle("~/bundles/global/plugins").Include(
                      "~/assets/global/plugins/backstretch/jquery.backstretch.min.js",
                      "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/assets/global/plugins/select2/js/select2.full.min.js"));
            
            //pages scripts
            bundles.Add(new ScriptBundle("~/bundles/global/master").Include(
                      "~/assets/global/scripts/app.js"));

            //layout js
            bundles.Add(new ScriptBundle("~/bundles/global/master/layout").Include(
                      "~/assets/global/layouts/layout4/scripts/layout.js",
                      "~/assets/global/layouts/global/scripts/quick-sidebar.min.js",
                      "~/assets/global/layouts/global/scripts/quick-nav.min.js"));

            //login page
            bundles.Add(new ScriptBundle("~/bundles/global/pages/login").Include(
                      "~/assets/pages/scripts/login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/assets/global/css/components-md.min.css",
                      "~/assets/global/css/plugins-md.min.css",
                      "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                      "~/assets/global/plugins/select2/css/select2.min.css",
                      "~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/layouts").Include(
                      "~/assets/global/layouts/layout4/css/layout.min.css",
                      "~/assets/global/layouts/layout4/css/themes/default.min.css",
                      "~/assets/global/layouts/layout4/css/custom.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                      "~/assets/pages/css/login.min.css"));
        }
    }
}
