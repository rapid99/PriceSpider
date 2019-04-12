using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PriceSpider.Web.Models
{
    public class ProductModel
    {
        public string Name { get; set; }

        public string Price { get; set; }

        [Required(ErrorMessage = "Manufactoring ID is required")]
        public string ManufactoringID { get; set; }

        public string SKU { get; set; }

        public string Company { get; set; }
    }
}
