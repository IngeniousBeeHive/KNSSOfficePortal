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
    public class BranchController : Controller
    {
        IBranchRepository branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Branch> branchlist = branchRepository.Get();
            return View(branchlist);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Branch branch = new Branch();
            return View(branch);
        }

        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            try
            {
                if (branch.IsValid)
                {
                    branchRepository.Add(branch);
                    return RedirectToActionPermanent("List");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(branch);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if(Id.HasValue)
            {
                Branch branch = branchRepository.Get(Id.Value);
                return View(branch);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(Branch branch)
        {
            try
            {
                if (branch.IsValid)
                {
                    branchRepository.Update(branch);
                    return RedirectToActionPermanent("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(branch);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                Branch branch = branchRepository.Get(Id.Value);
                return View(branch);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Delete(Branch branch)
        {
            try
            {
                branchRepository.Delete(branch.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(branch);
        }
    }
}