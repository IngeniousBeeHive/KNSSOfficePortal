using hmvtrust.core;
using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.hmvtrust.com.Controllers
{
    public class LibraryLogController : Controller
    {
        ILibraryLogRepository libraryLogRepository;


        public LibraryLogController(ILibraryLogRepository libraryLogRepository)
        {
            this.libraryLogRepository = libraryLogRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {           
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<LibraryLog> logList = libraryLogRepository.Get();
            return View(logList);
        }



        [HttpGet]
        public ActionResult Create()
        {
            LibraryLog logDetails = new LibraryLog();
            //ViewBag.BookCategory = bookCategoryRepository.Get();
            return View(logDetails);
        }

        [HttpPost]
        public ActionResult Create(LibraryLog logDetails)
        {
            try
            {
                if (logDetails.IsValid)
                {
                    libraryLogRepository.Add(logDetails);
                    return RedirectToActionPermanent("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(logDetails);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            //ViewBag.BookCategory = bookCategoryRepository.Get();
            if (Id.HasValue)
            {
                LibraryLog logDetails = libraryLogRepository.Get(Id.Value);
                return View(logDetails);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(LibraryLog logDetails)
        {
            try
            {
                if (logDetails.IsValid)
                {
                    libraryLogRepository.Update(logDetails);
                }
                List<LibraryLog> logList = libraryLogRepository.Get();
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(logDetails);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                LibraryLog logDetails = libraryLogRepository.Get(Id.Value);
                return View(logDetails);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Delete(LibraryLog logDetails)
        {
            try
            {
                libraryLogRepository.Delete(logDetails.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(logDetails);
        }
    }
}