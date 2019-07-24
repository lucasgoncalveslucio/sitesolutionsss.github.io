using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PontoMap.Models
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {


            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                //Extract the forms authentication cookie
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = authTicket.UserData;

                    // and now process your code as per your condition
                    string[] roles = authTicket.UserData.Split(',');

                    var userIdentity = new GenericIdentity(authTicket.Name);
                    var userPrincipal = new GenericPrincipal(userIdentity, roles);

                    filterContext.HttpContext.User = userPrincipal;
                    base.OnAuthorization(filterContext);

                }
            }
            string cookieName = FormsAuthentication.FormsCookieName;

            if (filterContext.HttpContext.Request.Cookies == null || filterContext.HttpContext.Request.Cookies[cookieName] == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            
        }
    }
}