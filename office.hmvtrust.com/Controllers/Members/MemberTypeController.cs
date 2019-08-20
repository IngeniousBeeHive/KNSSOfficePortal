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
    public class MemberTypeController : Controller
    {
        IMemberTypeRepository memberTypeRepository;

        public MemberTypeController(IMemberTypeRepository memberTypeRepository)
        {
            this.memberTypeRepository = memberTypeRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            List<MemberType> memberTypeList = memberTypeRepository.Get();
            return View(memberTypeList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            MemberType memberTypeList = new MemberType();
            return View(memberTypeList);
        }

        [HttpPost]
        public ActionResult Create(MemberType memberTypeList)
        {
            try
            {
                if (memberTypeList.IsValid)
                {
                    memberTypeRepository.Add(memberTypeList);
                    return RedirectToActionPermanent("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(memberTypeList);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            ViewBag.MemberListCategory = memberTypeRepository.Get();
            if (Id.HasValue)
            {
                MemberType memberType = memberTypeRepository.Get(Id.Value);
                return View(memberType);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(MemberType memberType)
        {
            try
            {
                if (memberType.IsValid)
                {
                    memberTypeRepository.Update(memberType);
                    return RedirectToActionPermanent("List");
                }
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(memberType);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                MemberType memberType = memberTypeRepository.Get(Id.Value);
                return View(memberType);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Delete(MemberType memberType)
        {
            try
            {
                memberTypeRepository.Delete(memberType.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(memberType);
        }

    }
}