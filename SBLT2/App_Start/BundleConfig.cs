using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SBLT2.App_Start
{

    public class BundleConfig
    {
        public static void RegisterBundles(System.Web.Optimization.BundleCollection bundles)
        {
            bundles.Add(new System.Web.Optimization.ScriptBundle("~/bundle/js")
            .Include("~/Scripts/JQWidgets/globalization/globalize.js",
             "~/Scripts/jquery-2.1.1.min.js",
             "~/Scripts/jquery-ui.min.js",
             "~/Scripts/JQWidgets/jqx-all.js",
             "~/Scripts/JQWidgets/jqxcore.js",
             "~/Scripts/jquery.toaster.js",
             "~/Scripts/JQWidgets/jqxdata.export.js",

             "~/Scripts/JQWidgets/jqxgrid.export.js"

            ));



            bundles.Add(new System.Web.Optimization.ScriptBundle("~/bundle/js1")
.Include(
 "~/scripts/bootstrap.min.js",
 "~/scripts/raphael-min.js",
  "~/plugins/morris/morris.min.js",
       "~/plugins/sparkline/jquery.sparkline.min.js",
"~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
       "~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
       "~/plugins/knob/jquery.knob.js",


        "~/scripts/moment.min.js",
 "~/plugins/daterangepicker/daterangepicker.js",
  "~/plugins/datepicker/bootstrap-datepicker.js",
       "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
"~/plugins/slimScroll/jquery.slimscroll.min.js",
       "~/plugins/fastclick/fastclick.min.js",
       "~/scripts/app.min.js"
));




            
            IItemTransform cssFixer = new CssRewriteUrlTransform();

            bundles.Add(
            new System.Web.Optimization.StyleBundle("~/bundle/css")
            .Include("~/Content/bootstrap.min.css", cssFixer)
               .Include("~/Content/JQWidgets/jqx.base.css", cssFixer)
            .Include("~/Content/fa/font-awesome.min.css", cssFixer)
            .Include("~/Content/ionic/ionicons.min.css", cssFixer)
            .Include("~/Content/AdminLTE.css", cssFixer)
            .Include("~/Content/skins/_all-skins.min.css", cssFixer)
            .Include("~/plugins/iCheck/flat/blue.css", cssFixer)
            .Include("~/plugins/morris/morris.css", cssFixer)
            .Include("~/plugins/jvectormap/jquery-jvectormap-1.2.2.css", cssFixer)
            .Include("~/plugins/datepicker/datepicker3.css", cssFixer)
            .Include("~/plugins/daterangepicker/daterangepicker-bs3.css", cssFixer)
            .Include("~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css", cssFixer)
            .Include("~/Content/Style.css", cssFixer)
            );




            BundleTable.EnableOptimizations = true;



        }
    }
}