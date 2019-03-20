using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using MariGold.OpenXHTML;


namespace MariGoldConverter
{
    // https://github.com/kannan-ar/MariGold.OpenXHTML
    class Program
    {
        static void Main(string[] args)
        {
            // Get HTML from website
            string html = string.Empty;
            string url = "https://wakeupandcode.com/key-vault-for-asp-net-core-web-apps";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            //Console.WriteLine(html);

            // Get content
            HtmlDocument htmlDoc = new HtmlDocument();
            //htmlDoc.Load(@"file.htm");
            htmlDoc.LoadHtml(html);

            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]");
            string nodeContent = (node == null) ? "Error, id not found" : node.InnerHtml;


            Console.WriteLine("Hello World!");

            // Create DOCX file
            WordDocument wordDoc = new WordDocument("sample.docx");
            wordDoc.Process(new HtmlParser(nodeContent));
            wordDoc.Save();

            //WordDocument doc = new WordDocument("blog.docx");
            //doc.BaseURL = "http:\\wakeupandcode.com";
            //doc.Process(new HtmlParser("<a href=\"index.htm\">sample</a>"));
            //doc.Save();


        }
    }
}
