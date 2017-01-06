using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_net_mvc.ViewModels.Store;

namespace asp_net_mvc.Controllers
{
    public class StoreController : Controller
    {
        // GET: /Store/Index
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.products = new List<ProductViewModel>();
            model.products.Add(new ProductViewModel {
                id = 1,
                name = "0.01uF/10nF 3kV Ceramic Capacitor",
                description = "High Voltage",
                unitPrice = 2.5M,
                imageUrl = "/images/120x120/3kVCeramicCapacitor.jpg",
                quantityInStock = 140
            });
            return View(model);
        }
    }
}