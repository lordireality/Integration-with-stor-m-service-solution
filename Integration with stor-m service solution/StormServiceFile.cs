using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace StormIntegration
{
    public class StormFileService
    {
        public class Instance
        {
            public string FormJson(IDictionary<string, string> files)
            {

                return Newtonsoft.Json.JsonConvert.SerializeObject(getFileStructure(files));
            }



            public Models.Files getFileStructure(IDictionary<string, string> files)
            {
                var _fileStruct = new Models.Files();
                foreach(var elem in files)
                {
                    Console.Write(elem.Key+ ": " + elem.Value);
                    Byte[] fileBytes = File.ReadAllBytes(elem.Key);
                    _fileStruct.files.Add(new Models.File(elem.Value, Convert.ToBase64String(fileBytes)));
                }
                return _fileStruct;
            }
        }

        public class Models
        {
            public class Files
            {
                public string name = "ТУ #1 от 01.01.1970";
                public string number = "1";
                public string date = "01-01-70";
                public string customer = "Рога И Копыта, ООО";
                public string issuingOrganization = "kis gov";
                public string objectOfBuilding = "194214, г. Санкт-Петербург, Пр-кт Энгельса д. 120.";
                public string numberOfKadastr = "8888:8888";


                public List<File> files = new List<Models.File>();
            }
            public class File
            {
                public string fileName;
                public string binaryData;

                public File(string fileName, string binaryData)
                {
                    this.fileName = fileName;
                    this.binaryData = binaryData;
                }
            }
        }




    }
}
