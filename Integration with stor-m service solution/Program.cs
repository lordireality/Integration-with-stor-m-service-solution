using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration_with_stor_m_service_solution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filesList = new Dictionary<string, string>();
            filesList.Add("C:\\test1.txt", "test1.txt");
            filesList.Add("C:\\test2.txt", "test2.txt");
            filesList.Add("C:\\test3.txt", "test3.txt");
            string json = new StormIntegration.StormFileService.Instance().FormJson(filesList);
            Console.Write(json);


            var provider = new StormIntegration.HttpProvider().MakeRequest("http://192.168.161.4:8080/api/v1/setcard", StormIntegration.RequestType.POST, null, json);
            Console.WriteLine(provider.success);
            Console.ReadKey();
        }
    }
}
