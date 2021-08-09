using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreFunc31
{
    public static class BlobBindingFunc
    {
        [FunctionName("BlobBindingFunc")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Blob("demoblobs/MapResult.xml", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream inputFile,
            [Blob("demoblobs/FuncXML.xml", FileAccess.Write ,Connection = "AzureWebJobsStorage")] Stream outputFile,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            XmlDocument xmldc = new XmlDocument();

            xmldc.Load(inputFile) ;
            //JsonConvert.SerializeXmlNode(xmldc);
            var root = xmldc.DocumentElement;
            var nods = xmldc.DocumentElement?.ChildNodes;
            //-----------------------------------------------------------------
            // The below line helps to return the list of all the tags with the specified name, return type at the top needs to be XmlNodeList
            //var i = root?.GetElementsByTagName("Email");


     
            foreach (XmlNode i in nods)
            {
               
               Employee.Add(i.FirstChild.InnerText, i.ChildNodes[1]?.InnerText,i.LastChild.InnerText);
                // This loop adds all three items into lists parameters of Employee class. 
            }


            // Below code is used to copy the parameters to a new file. 


            var doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
            doc.AppendChild(xmlDeclaration);
            XmlElement employeelist = doc.CreateElement("employeeList");
            doc.AppendChild(employeelist);
            int q = 0;

            // Code below defines the structure of output xml
            foreach (var l in Employee.firstName)
            {
                XmlElement emp1 = doc.CreateElement("employee");
                employeelist.AppendChild(emp1);

                XmlElement e2 = doc.CreateElement("firstname");
                e2.InnerText = l;
                emp1.AppendChild(e2);
                
                XmlElement e5 = doc.CreateElement("lastname");
                e5.InnerText = Employee.lastName[q];
                emp1.AppendChild(e5);

                XmlElement e3 = doc.CreateElement("address");
                e3.InnerText = Employee.CompleteAddress[q];
                emp1.AppendChild(e3);

                XmlElement e4 = doc.CreateElement("email");
                emp1.AppendChild(e4);

                XmlAttribute e7 = doc.CreateAttribute("emailatt");
                e7.InnerText = Employee.Email[q];
                e4.Attributes.Append(e7);
                // Attribute of Email tag

                XmlElement e6 = doc.CreateElement("contact");
                e6.InnerText = Employee.Phone[q];
                e4.AppendChild(e6);
                //Child of Email tag

                

                q++;
            }
            doc.Save(outputFile);

            var j = Employee.Show();
            
            return j[6];//returning one of the Empployee Address using the Show method in Employee Class. 
        }
    }
}
