using hmvtrust.core;
using hmvtrust.core.Entities;
using office.hmvtrust.com.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static hmvtrust.core.Util;

namespace office.hmvtrust.com.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
         IMemberRepository memberRepository;
        IMemberTypeRepository memberTypeRepository;
        IBranchRepository branchRepository;

        public MemberController(IMemberRepository memberRepository,IMemberTypeRepository memberTypeRepository, IBranchRepository branchRepository)
        {
            this.memberRepository = memberRepository;
            this.memberTypeRepository = memberTypeRepository;
            this.branchRepository = branchRepository;
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Member> memberList = memberRepository.Get();
            return View(memberList);
        }

        [HttpGet]
        public ActionResult View(int? Id)
        {
            if (Id.HasValue)
            {
                Member member = memberRepository.Get(Id.Value);
                return View(member);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpGet]
        public ActionResult Index(int? searchtype, string optionval)
        {
            List<Member> memberForm = new List<Member>();
            if (searchtype.HasValue && !string.IsNullOrEmpty(optionval) && searchtype.Value != 0)
            {
                if (searchtype.Value == 1)
                {
                    int memberid = int.Parse(optionval);
                    memberForm = memberRepository.GetList(d => d.FamilyNo == memberid);

                    if (memberForm == null)
                        ViewBag.Error = "No such FamilyNo exists";
                }
                else if (searchtype.Value == 2)
                {                    
                    memberForm = memberRepository.GetList(d => d.MemberName.Contains(optionval));

                    if (memberForm.Count == 0)
                    {
                        ViewBag.Error = "No such Member exists";
                    }
                }
                else if (searchtype.Value == 3)
                {                    
                    memberForm = memberRepository.GetList(d => d.MemberTypeId.MemberTypeName.Contains(optionval));

                    if (memberForm.Count == 0)
                    {
                        ViewBag.Error = "No such MemberType exists";
                    }
                }
            }
            return View(memberForm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Member memberDetails = new Member();
            ViewBag.MemberType = memberTypeRepository.Get();
            ViewBag.Branch = branchRepository.Get();
            return View(memberDetails);
        }

        [HttpPost]
        public ActionResult Create(Member memberDetails, HttpPostedFileBase[] memberphoto, DocumentData documentData)
        {            
            try
            {
                if (memberDetails.IsValid)
                {
                    if (memberDetails.FamilyNo == 0)
                    {
                        ViewBag.Error = "Require Family Number to Continue";
                        return RedirectToActionPermanent("Create");
                    }
                    else if (memberDetails.Photo is null)
                    {                       
                        memberDetails.Status = true;
                        if (documentData.memberphoto != null)
                        {
                            MemoryStream memStream = Util.ToMemoryStream(documentData.memberphoto.InputStream, documentData.memberphoto.ContentLength);
                            Media media = Util.Save(memStream, documentData.memberphoto.FileName, MediaType.MEMBERPHOTO, memberDetails.FamilyNo, memberDetails.MemberName.ToString());

                            if (media != null)
                            {
                                memberDetails.Photo = media.FileName;
                            }
                        }
                        memberRepository.Add(memberDetails);
                        return RedirectToActionPermanent("List");
                    }
                }        
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Invalid Entry";
            }
            return View(memberDetails);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            ViewBag.Branch = branchRepository.Get();
            ViewBag.MemberType = memberTypeRepository.Get();
            if (Id.HasValue)
            {
                Member memberDetails = memberRepository.Get(Id.Value);
                return View(memberDetails);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Edit(Member memberDetails,DocumentData documentData)
        {
            try
            {
                if (memberDetails.IsValid)
                {
                    if (documentData.memberphoto != null)
                    {
                        MemoryStream memStream = Util.ToMemoryStream(documentData.memberphoto.InputStream, documentData.memberphoto.ContentLength);
                        Media media = Util.Save(memStream, documentData.memberphoto.FileName, MediaType.MEMBERPHOTO, memberDetails.FamilyNo, memberDetails.MemberName.ToString());

                        if (media != null)
                        {
                            memberDetails.Photo = media.FileName;
                        }
                    }
                    memberRepository.Update(memberDetails);
                    List<Member> memberlist = null;
                    memberlist = memberRepository.Get();
                    return RedirectToActionPermanent("List");
                }
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(memberDetails);
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            if (Id.HasValue)
            {
                Member memberDetails = memberRepository.Get(Id.Value);
                return View(memberDetails);
            }
            return RedirectToActionPermanent("list");
        }

        [HttpPost]
        public ActionResult Delete(Member memberDetails)
        {
            try
            {
                memberRepository.Delete(memberDetails.Id);
                return RedirectToActionPermanent("List");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(memberDetails);
        }

        [HttpGet]
        public ActionResult FindBloodMatch(string optionval)
        {
           
            List<Member> memberForm = new List<Member>();
            List<Member> memberList = memberRepository.Get();

            if (!string.IsNullOrEmpty(optionval))
            {
                memberForm = memberList.FindAll(d => d.BloodGroup == optionval);
                if (memberForm.Count == 0)
                    ViewBag.Error = "Searched BloodGroup does not exists";
            }
            return View(memberForm);
        }

    }
}