using hmvtrust.core;
using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.hmvtrust.com.Controllers
{
    [Authorize]
    public class BookCategoryController : Controller
    {
        IBookCategoryRepository bookCategoryRepository;


        public BookCategoryController(IBookCategoryRepository bookCategoryRepository)
        {
            this.bookCategoryRepository = bookCategoryRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            List<BookCategory> bookCategoryList = bookCategoryRepository.Get();
            return View(bookCategoryList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BookCategory bookCategory = new BookCategory();
            return View(bookCategory);
        }

        [HttpPost]
        public ActionResult Create(BookCategory bookCategory)
        {
            try
            {
                if (bookCategory.IsValid)
                {
                    bookCategoryRepository.Add(bookCategory);
                    return RedirectToActionPermanent("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookCategory);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id.HasValue)
            {
                BookCategory bookCategory = bookCategoryRepository.Get(Id.Value);
                return View(bookCategory);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(BookCategory bookCategory)
        {
            try
            {
                if (bookCategory.IsValid)
                {
                    bookCategoryRepository.Update(bookCategory);

                    return RedirectToActionPermanent("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookCategory);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                BookCategory bookCategory = bookCategoryRepository.Get(Id.Value);
                return View(bookCategory);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Delete(BookCategory bookCategory)
        {
            try
            {
                bookCategoryRepository.Delete(bookCategory.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookCategory);
        }
    }
}