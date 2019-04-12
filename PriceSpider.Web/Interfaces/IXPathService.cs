using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PriceSpider.Web.Interfaces
{
    public interface IXPathService
    {
        int GetElementCount(string xPathQuery, HtmlDocument document);
        string GetElementText(string xPathQuery, HtmlDocument document);
    }
}
