using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.hmvtrust.com.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        IMemberRepository memberRepository;

        public HomeController(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {     
                ViewBag.BirthdayList = memberRepository.Get();
            }
            else if (User.IsInRole("Librarian"))
            {
                return RedirectPermanent("/librarylog/index");
            }

            return View();
        }        
    }
}