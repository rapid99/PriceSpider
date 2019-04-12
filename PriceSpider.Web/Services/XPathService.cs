using PriceSpider.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PriceSpider.Web.Services
{
    public class XPathService : IXPathService
    {
        public int GetElementCount(string xPathQuery, HtmlDocument document)
        {
            return document.DocumentNode.SelectNodes(xPathQuery).Count;
        }

        public string GetElementText(string xPathQuery, HtmlDocument document)
        {
            return document.DocumentNode.SelectSingleNode(xPathQuery).InnerText;
        }
    }
}
