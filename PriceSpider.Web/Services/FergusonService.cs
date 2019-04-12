using PriceSpider.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using PriceSpider.Web.Models;
using PriceSpider.Web.Services;

namespace PriceSpider.Web.Services
{
    public class FergusonService : IFergusonService
    {
        private readonly IXPathService _xPathService;

        public FergusonService(IXPathService xPathService)
        {
            _xPathService = xPathService;
        }

        public async Task<List<ProductModel>> Search(string input)
        {
            var resultList = new List<ProductModel>();

            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://www.ferguson.com/category?Ntt=" + input + "&searchKeyWord=" + input);
                var pageContents = await response.Content.ReadAsStringAsync();

                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);

                var itemCount = _xPathService.GetElementCount("(//div[contains(@class, 'prod-name')]//p)", pageDocument);
               
                for (var i = 1; i <= itemCount; i++)
                {
                    var model = new ProductModel();

                    model.Company = "Ferguson";
                    model.Name = _xPathService.GetElementText("(//div[contains(@class, 'prod-name')]//p)[" + i + "]", pageDocument).Replace("&amp174;", string.Empty) ?? string.Empty;
                    model.Price = _xPathService.GetElementText("(//p[contains(@class, 'price')])[" + i + "]" , pageDocument) ?? string.Empty;

                    resultList.Add(model);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return resultList;
        }

    }
}
