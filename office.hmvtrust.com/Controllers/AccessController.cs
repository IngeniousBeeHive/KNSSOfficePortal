using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.hmvtrust.com.Controllers
{
    [Authorize]
    public class AccessController : Controller
    {
        IUserRepository userRepository;
        IRoleRepository roleRepository;

        public AccessController(IUserRepository userRepository
                                ,IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            List<User> userlist = userRepository.Get();
            return View(userlist);
        }

        [HttpGet]
        public ActionResult Create()
        {
            User user = new User();
            var userroles = roleRepository.Get();
            ViewBag.Roles = userroles;
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user, IEnumerable<int> userroles)
        {
            var allroles = roleRepository.Get();
            ViewBag.Roles = allroles;

            user.UserName = user.Email;

            if (user.IsValid)
            {
                try
                {
                    if (userroles != null)
                    {
                        foreach (int i in userroles)
                        {
                            user.Roles.Add(allroles.First(d => d.Id == i));
                        }
                        var u = userRepository.Add(user);

                        if (u != null)
                        {
                            ViewBag.Sucess = "User Saved Sucessfully";
                            return RedirectToActionPermanent("List");
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Select a Role for the User";
                    }
                }
                catch (Exception excp)
                {
                    if (excp.InnerException != null)
                        ViewBag.Error = excp.InnerException.Message.Contains("UQ") ? "Email / MobileNumber has to be unique" : excp.Message;
                    else
                        ViewBag.Error = excp.Message;
                }
            }
            else
            {
                var valresults = user.Errors();
                foreach (var v in valresults)
                {
                    ModelState.AddModelError(v.MemberNames.First(), v.ErrorMessage);
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id.HasValue && Id > 0)
            {
                User user = userRepository.Get(Id.Value);
                if (user != null)
                {
                    ViewBag.Roles = roleRepository.Get();
                    return View(user);
                }
                else
                    return RedirectToActionPermanent("List");
            }
            return RedirectToActionPermanent("List");
        }

        [HttpPost]
        public ActionResult Edit(User user, IEnumerable<int> userroles)
        {
            var allroles = roleRepository.Get();
            ViewBag.Roles = allroles;
            user.UserName = user.Email;
            if (user.IsValid)
            {
                try
                {
                    if (userroles != null)
                    {
                        user.Roles.Clear();
                        foreach (int i in userroles)
                        {
                            user.Roles.Add(allroles.First(d => d.Id == i));
                        }

                        var u = userRepository.Update(user);
                        if (u != null)
                        {
                            return RedirectToActionPermanent("List", new { Id = u.Id });
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Select a Role for the User";
                    }
                }
                catch (Exception excp)
                {
                    if (excp.InnerException != null)
                        ViewBag.Error = excp.InnerException.Message.Contains("UQ_") ? "Email / Mobile number has to be unique" : excp.Message;
                    else
                        ViewBag.Error = excp.Message;
                }
            }
            else
            {
                var valresults = user.Errors();

                foreach (var v in valresults)
                {
                    ModelState.AddModelError(v.MemberNames.First(), v.ErrorMessage);
                }

            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue && Id > 0)
            {
                User user = userRepository.Get(Id.Value);
                if (user != null)
                {
                    ViewBag.Roles = roleRepository.Get();
                    return View(user);
                }
                else
                    return RedirectToActionPermanent("List");
            }
            return RedirectToActionPermanent("List");
        }

        [HttpPost]
        public ActionResult Delete(User user)
        {
            try
            {
                userRepository.Delete(user.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(user);
        }
    }
}