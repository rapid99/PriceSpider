using PriceSpider.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PriceSpider.Web.Models;
using System.Net.Http;

namespace PriceSpider.Web.Services
{
    public class SupplyHouseService : ISupplyHouseService
    {
        private readonly IXPathService _xPathService;

        public SupplyHouseService(IXPathService xPathService)
        {
            _xPathService = xPathService;
        }

        public async Task<List<ProductModel>> Search(string input)
        {
            var resultList = new List<ProductModel>();

            try
            {
                HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                });

                var response = await client.GetAsync("https://www.supplyhouse.com/sh/control/search/~SEARCH_STRING=" + input + "?searchText=" + input);
                var pageContents = await response.Content.ReadAsStringAsync();

                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);

                var itemCount = _xPathService.GetElementCount("(//div[contains(@class, 'desc')])", pageDocument);

                for (var i = 1; i <= itemCount; i++)
                {
                    var model = new ProductModel();

                    model.Company = "Supply House";
                    model.Name = _xPathService.GetElementText("(//div[(@class, 'desc')]//a)[1]", pageDocument);
                    model.Price = _xPathService.GetElementText("(//div[(@class, 'unit-price')]//span)[1]", pageDocument);

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