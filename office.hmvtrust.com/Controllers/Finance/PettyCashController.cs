using hmvtrust.core;
using hmvtrust.core.Entities;
using office.hmvtrust.com.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.hmvtrust.com.Controllers.Finance
{
    [Authorize]
    public class PettyCashController : Controller
    {
        IPettyCashRepository pettyCashRepository;

        public PettyCashController(IPettyCashRepository pettyCashRepository)
        {
            this.pettyCashRepository = pettyCashRepository;
            
        }

        [HttpGet]
        public ActionResult List()
        {
            List<PettyCash> cashList = pettyCashRepository.Get();
            return View(cashList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PettyCash pettyCash = new PettyCash();
            return View(pettyCash);
        }

        [HttpPost]
        public ActionResult Create(PettyCash pettyCashDetails, HttpPostedFileBase[] memberphoto, DocumentData documentData)
        {
            try
            {
                if (pettyCashDetails.IsValid)
                {
                    if (pettyCashDetails.AttachedFiles is null)
                    {
                        if (documentData.memberphoto != null)
                        {
                            MemoryStream memStream = Util.ToMemoryStream(documentData.memberphoto.InputStream, documentData.memberphoto.ContentLength);
                            Media media = Util.SaveReceipt(memStream, documentData.memberphoto.FileName, MediaType.RECEIPTPHOTO);                     
                            

                            if (media != null)
                            {
                                pettyCashDetails.AttachedFiles = media.FileName;
                            }
                        }
                        pettyCashRepository.Add(pettyCashDetails);
                        return RedirectToActionPermanent("List");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Invalid Entry";
            }
            return View(pettyCashDetails);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id.HasValue)
            {
                PettyCash pettyCash = pettyCashRepository.Get(Id.Value);
                return View(pettyCash);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(PettyCash pettyCash)
        {
            try
            {
                if (pettyCash.IsValid)
                {
                    pettyCashRepository.Update(pettyCash);
                    return RedirectToActionPermanent("List");
                }
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(pettyCash);
        }
    }
}