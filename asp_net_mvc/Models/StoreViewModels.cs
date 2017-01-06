using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_net_mvc.ViewModels.Store
{
    public class IndexViewModel
    {
        public IList<ProductViewModel> products { get; set; }
    }

    // Product View Model to appear on the Store Index Page
    public class ProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal unitPrice { get; set; }
        public string imageUrl { get; set; }
        public int quantityInStock { get; set; }
    }
}