using System.Web;
using System.Web.Optimization;

namespace PontoMap
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/customScripts").Include(
            "~/Scripts/CustomValidations.js",
            "~/Scripts/bootstrap-notify.min.js",
            "~/Scripts/moment.js",
            "~/Scripts/vanilla-masker.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      , "~/Content/site.css"
                     ));


            bundles.Add(new StyleBundle("~/bundles/customCss").Include(
                "~/Content/formulario.css",
                "~/Content/mobileStores.css"));
            
            //Css e js do tema ===================
            bundles.Add(new StyleBundle("~/bundles/nineStarsCss").Include(
                "~/Content/Ninestars/css/bootstrap.min.css",
                //"~/Content/Ninestars/font-awesome/css/font-awesome.min.css",
                "~/Content/Ninestars/css/nivo-lightbox.css",
                "~/Content/Ninestars/css/nivo-lightbox-theme/default/default.css",
                "~/Content/Ninestars/css/animate.css",
                "~/Content/Ninestars/css/style.css",
                "~/Content/Ninestars/color/default.css",
                "~/Content/jquery-ui/jquery-ui.min.css",
                "~/Content/jquery-ui/jquery-ui.theme.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/nineStarsJs").Include(
                "~/Content/Ninestars/js/jquery.easing.min.js",
                "~/Content/Ninestars/js/classie.js",
                "~/Content/Ninestars/js/gnmenu.js",
                "~/Content/Ninestars/js/jquery.scrollTo.js",
                "~/Content/Ninestars/js/nivo-lightbox.min.js",
                "~/Content/Ninestars/js/stellar.js",
                "~/Content/Ninestars/js/custom.js",
                "~/Content/jquery-ui/jquery-ui.min.js"
                ));


        }
    }
}
