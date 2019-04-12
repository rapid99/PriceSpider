using PriceSpider.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceSpider.Web.Interfaces
{
    public interface ISupplyHouseService
    {
        Task<List<ProductModel>> Search(string input);
    }
}
