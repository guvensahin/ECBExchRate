using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ECBExchRate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Parse("2022-03-16");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml"); // last 90 days


            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("x", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");


            string dateStr = dateTime.ToString("yyyy-MM-dd");
            string xpath = string.Format("//x:Cube[@time='{0}']", dateStr);

            XmlNode nodeDay = xmlDoc.SelectSingleNode(xpath, nsmgr);

            XmlNodeList nodeList = nodeDay.ChildNodes;


            // show on console
            Console.WriteLine(dateStr);

            foreach (XmlNode nodeCurrency in nodeList)
            {
                Console.WriteLine(String.Format("Currency: {0}, ExchRate: {1}",
                    nodeCurrency.Attributes["currency"].Value,
                    nodeCurrency.Attributes["rate"].Value));
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
