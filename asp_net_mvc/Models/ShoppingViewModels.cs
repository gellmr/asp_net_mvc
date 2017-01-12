using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_net_mvc.Models.ShoppingViewModels
{

    public class StoreViewModel {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public string SearchString { get; set; }
        public OrderViewModel Order { get; set; }
        public StoreViewModel(){
            Order = new OrderViewModel();
            CurrentPage = 1;
            SearchString = "your search string";
        }
    }

    public class CartViewModel {
        public OrderViewModel Order { get; set; }
        public CartViewModel() { Order = new OrderViewModel(); }
    }

    public class CheckoutViewModel {
        public OrderViewModel Order { get; set; }
        public CheckoutViewModel() { Order = new OrderViewModel(); }
    }
    
    // collection of products to order, the total number of lines, the total number of items
    public class OrderViewModel {
        public OrderViewModel() {
            Products = new List<OrderedProductViewModel>();
        }
        public IList<OrderedProductViewModel> Products { get; set; }
        // Order summary info
        public int TotalLines { get; set; }     // eg 2     (3 resistors + 1 spanner = 2 lines)
        public decimal TotalItems { get; set; } // eg 4.    (3 resistors + 1 spanner = 4 items)
        public decimal GrandTotal { get; set; } // eg 35.00 ($30.00 for the resistors + $5.00 for the spanner)
    }

    // a product, how many you want, and the cost of this.
    public class OrderedProductViewModel {
        public ProductViewModel Product { get; set; }
        public int QuantityToOrder { get; set; }    // eg 3
        public decimal SubTotal { get; set; }       // eg 30.00
    }

    public class ProductViewModel {
        public string Name { get; set; }            // "Chrome plated spanner"
        public string Description { get; set; }     // "High density drop forged steel coated in a protective high density zinc alloy"
        public int QuantityInStock { get; set; }    // 140
        public string ImageUrl { get; set; }        // "/images/120x120/missing.jpg"
        public int Id { get; set; }                 // 1
        public decimal UnitPrice { get; set; }      // eg 10.00
    }
}