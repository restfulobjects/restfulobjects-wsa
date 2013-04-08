using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using RunMvc.App_Start;
using NakedObjects.Web.Mvc;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (NakedObjectsStart), "PreStart")]
[assembly: PostApplicationStartMethod(typeof (NakedObjectsStart), "PostStart")]

namespace RunMvc.App_Start {
    public static class NakedObjectsStart {
        public static void PreStart() {
            InitialiseLogging();

            RegisterRoutes(RouteTable.Routes);
        }

        public static void PostStart() {
            RegisterBundles(BundleTable.Bundles);

            RunWeb.Run();
            DependencyResolver.SetResolver(new NakedObjectsDependencyResolver());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
            routes.IgnoreRoute("{*nakedobjects}", new {nakedobjects = @"(.*/)?nakedobjects.ico(/.*)?"});

            NakedObjects.Web.Mvc.RunMvc.RegisterGenericRoutes(routes);
        }

        // this may be moved to BundleConfig as required - here just to simplify Nuget install  
        public static void RegisterBundles(BundleCollection bundles) {
            // register Naked Objects bundles  

            bundles.Add(new ScriptBundle("~/bundles/nakedobjectsajax").Include(
                "~/Scripts/jquery.address-{version}.js",
                "~/Scripts/jquery.json-{version}.js",
                "~/Scripts/jstorage*",
                "~/Scripts/NakedObjects-Ajax*"));

            bundles.Add(new ScriptBundle("~/bundles/nakedobjectsbasic").Include(
                "~/Scripts/NakedObjects-Basic*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydatepicker").Include(
                "~/Scripts/ui/i18n/jquery.ui.datepicker-en-GB*"));

            bundles.Add(new StyleBundle("~/Content/nakedobjectscss").Include(
                "~/Content/NakedObjects.css"));
        }

        public static void InitialiseLogging() {
            // uncomment and add appropriate Common.Logging package
            // http://netcommon.sourceforge.net/docs/2.1.0/reference/html/index.html

            //var properties = new NameValueCollection();
        
            //properties["configType"] = "INLINE";
            //properties["configFile"] = @"C:\Naked Objects\nologfile.txt";

            //LogManager.Adapter = new Common.Logging.Log4Net.Log4NetLoggerFactoryAdapter(properties);

        }
    }
}