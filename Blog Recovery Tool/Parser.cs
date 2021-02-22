using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Blog_Recovery_Tool
{
    public class Parser
    {

        HtmlWeb web = new HtmlWeb();
        HtmlDocument page = null;
        public Parser(string url)
        {
            page = web.Load(url);
        }
        //public string ImageUrl() //the first image from the post, just for display
        //{
        //    return page.DocumentNode.SelectSingleNode(@"//*[@id=""Blog1""]/div[1]/div/div/div/div[1]/meta[1]").Attributes["content"].Value;
        //}
        public string Main()
        {
            return page.DocumentNode.SelectSingleNode(@"//div[@class=""post-body entry-content""]").OuterHtml;
        }
        public string Title()
        {
            return page.DocumentNode.SelectSingleNode(@"//*[@id=""Blog1""]/div[1]/div/div/div/div[1]/h3").InnerText;
        }
    }
}
