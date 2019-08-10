using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace office.hmvtrust.com
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie =
                          Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && (!string.IsNullOrEmpty(authCookie.Value)))
            {
                FormsAuthenticationTicket authTicket =
                                            FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(',');
                GenericPrincipal userPrincipal =
                                 new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                Context.User = userPrincipal;

                if (roles.Count() == 0)
                {
                    Response.RedirectPermanent("/");
                }
            }
        }
    }
}