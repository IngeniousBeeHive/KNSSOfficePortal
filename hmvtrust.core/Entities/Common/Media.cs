using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core.Entities
{
    public enum MediaType
    {
        MEMBERPHOTO        
    }
    public class Media
    {
        public string FileName { get; set; }
        public MediaType MediaType { get; set; }

        public string LocalFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["documentpath"] + "\\" + this.MediaType.ToString() + "\\" + FileName;
            }
        }
    }
}
