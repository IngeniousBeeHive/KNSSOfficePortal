using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.hmvtrust.com.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        IBookRepository bookRepository;
        IBookCategoryRepository bookCategoryRepository;

        public BooksController(IBookRepository bookRepository, IBookCategoryRepository bookCategoryRepository)
        {
            this.bookRepository = bookRepository;
            this.bookCategoryRepository = bookCategoryRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Book> booksList = bookRepository.Get();
            return View(booksList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Book bookDetails = new Book();
            ViewBag.BookCategory = bookCategoryRepository.Get();
            return View(bookDetails);
        }

        [HttpPost]
        public ActionResult Create(Book bookDetails)
        {
            try
            {
                if (bookDetails.IsValid)
                {
                    bookRepository.Add(bookDetails);
                    return RedirectToActionPermanent("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookDetails);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            ViewBag.BookCategory = bookCategoryRepository.Get();
            if (Id.HasValue)
            {
                Book bookDetails = bookRepository.Get(Id.Value);
                return View(bookDetails);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(Book bookDetails)
        {
            try
            {
                if (bookDetails.IsValid)
                {
                    bookRepository.Update(bookDetails);
                    return RedirectToActionPermanent("List");
                }
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookDetails);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                Book bookDetails = bookRepository.Get(Id.Value);
                return View(bookDetails);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Delete(Book bookDetails)
        {
            try
            {
                bookRepository.Delete(bookDetails.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bookDetails);
        }
    }
}