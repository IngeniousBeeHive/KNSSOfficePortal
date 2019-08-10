using hmvtrust.core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace office.hmvtrust.com.Controllers
{
    public class AuthenticateController : Controller
    {
        IOfficeAuthenticationManager officeAuthenticationManager;

        public AuthenticateController(IOfficeAuthenticationManager officeAuthenticationManager)
        {
            this.officeAuthenticationManager = officeAuthenticationManager;
        }

        public ActionResult Index(string userName, string password)
        {
            if (Request.HttpMethod == "POST")
            {
                if (!(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)))
                {
                    try
                    {
                        var user = officeAuthenticationManager.Authenticate(userName, password);
                        if (user != null)
                        {
                            string rolescsv = "";

                            foreach (var role in user.Roles)
                            {
                                rolescsv += role.Name + ",";
                            }

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                                         1,
                                                                         user.Id.ToString(),  //user id
                                                                         DateTime.Now,
                                                                         DateTime.Now.AddMinutes(20),  // expiry
                                                                         false,  //do not remember
                                                                         rolescsv,
                                                                         "/");
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                                                 FormsAuthentication.Encrypt(authTicket));
                            Response.Cookies.Add(cookie);
                            return Redirect("~/home");

                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Authentication Failed";
                        }
                    }
                    catch (Exception exp)
                    {
                        ViewBag.ErrorMessage = exp.Message;
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "User name / password cannot be blank";
                }
            }
            else
            {
                FormsAuthentication.SignOut();
            }
            return View();
        }
    }
}