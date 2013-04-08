// Copyright © Naked Objects Group Ltd ( http://www.nakedobjects.net). 
// All Rights Reserved. This code released under the terms of the 
// Microsoft Public License (MS-PL) ( http://opensource.org/licenses/ms-pl.html) 
using System.Web.Mvc;
using NakedObjects.Web.Mvc.Controllers;
using NakedObjects.Web.Mvc.Models;

namespace RunMvc.Controllers {

    //[Authorize]
    public class SystemController : SystemControllerImpl {


        public override ActionResult Root() {
            return base.Root();
        }
        
        public override ActionResult Index() {
            return base.Index();
        }

        public override ActionResult AboutNakedObjects() {
            return base.AboutNakedObjects();
        }

        public override ActionResult ChangeLicence() {
            return base.ChangeLicence();
        }

        public override ActionResult LogOut() {
            return base.LogOut();
        }

        public override ActionResult Quit() {
            return base.Quit();
        }

        public override ActionResult ExecuteFixture(string name) {
            return base.ExecuteFixture(name);
        }

        [HttpPost]
        public override ActionResult ClearHistory(bool clearAll) {
            return base.ClearHistory(clearAll);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (Request.Browser.Type.ToUpper() == "IE6" || Request.Browser.Type.ToUpper() == "IE7") {
                filterContext.Result = View("BrowserError");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}