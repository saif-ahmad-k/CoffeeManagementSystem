using CoffeePricingMgt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoffeePricingMgt.Models
{
    public class ProductPricingForListVM
    {
        public List<tblProductPricing> ProductPricingList { get; set; } = new List<tblProductPricing>();
        [NotMapped]
        public DateTime? FromDate { get; set; }
        [NotMapped]
        public DateTime? ToDate { get; set; }
        [NotMapped]
        public int? ProductID { get; set; }
    }
}