using MobileShopOnline.App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileShopOnline.App.Controllers
{
    public class AuthorizedController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (AccountLogin)Session[Constants.USER_SESSION];
            if (user == null || !user.Role.Equals("client"))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Login", action = "Login" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}