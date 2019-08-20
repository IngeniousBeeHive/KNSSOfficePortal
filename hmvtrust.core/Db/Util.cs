using hmvtrust.core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core
{
     
    public class Util
    {
        public static MemoryStream ToMemoryStream(Stream stream, int length)
        {
            MemoryStream memoryStream = null;
            using (var binaryReader = new BinaryReader(stream))
            {
                var fileData = binaryReader.ReadBytes(length);
                memoryStream = new MemoryStream(fileData);

            }

            return memoryStream;
        }
        public static Media Save(MemoryStream data
                         , string fileName
                         , MediaType type
                         , int FamilyNo
                         , string MemberName)
        {
            Media media = null;
            string newfilename = string.Empty;

            if (data != null)
            {
                string storagePath = ConfigurationManager.AppSettings["documentpath"] + "\\" + type + "\\";

                if (!Directory.Exists(storagePath))
                    Directory.CreateDirectory(storagePath);

                FileInfo fi = new FileInfo(fileName);

                //Assign a uniquefile name 
                if (string.IsNullOrEmpty(MemberName))
                {
                    newfilename = Guid.NewGuid().ToString() + fi.Extension;
                }
                else
                {
                    newfilename = FamilyNo + "_" + MemberName + fi.Extension;
                }

                string currentpath = storagePath + "\\" + newfilename;

                if (File.Exists(storagePath + "\\" + newfilename))
                {
                    File.Copy(currentpath, storagePath + "\\" + newfilename + "_Old.jpg", true);
                }

                FileStream file = new FileStream(storagePath + "\\" + newfilename, FileMode.Create, FileAccess.Write);
                data.WriteTo(file);
                file.Close();
                data.Close();

                Media m = new Media();
                m.FileName = newfilename;
                m.MediaType = type;
                media = m;
            }
            return media;
        }
    }
}
