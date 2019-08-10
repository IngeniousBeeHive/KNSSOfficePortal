using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office.hmvtrust.com.Models
{
    public class DocumentData
    {
        public HttpPostedFileBase studentphoto { get; set; }
        public HttpPostedFileBase fatherphoto { get; set; }
        public HttpPostedFileBase motherphoto { get; set; }
        public HttpPostedFileBase guardianphoto { get; set; }
        public HttpPostedFileBase birthcertificate { get; set; }
        public HttpPostedFileBase medicalform { get; set; }
        public HttpPostedFileBase aadharcard { get; set; }
        public HttpPostedFileBase markscard { get; set; }
        public HttpPostedFileBase transfercertificate { get; set; }
    }
}