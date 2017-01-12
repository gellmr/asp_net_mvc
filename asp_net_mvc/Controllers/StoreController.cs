using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_net_mvc.Models.ShoppingViewModels;
using asp_net_mvc.Helpers;

namespace asp_net_mvc.Controllers
{
    public class StoreController : Controller
    {
        private string ImagesPath;

        public StoreController(){
            ImagesPath = ApplicationHelper.ImagesPath();
        }

        // GET: /Store/Index
        public ActionResult Index()
        {
            StoreViewModel model = new StoreViewModel();

            // placeholder products until i make a database
            model.Order.Products.Add( new OrderedProductViewModel {
                Product = new ProductViewModel {
                    Id = 1,
                    Name = "0.01uF/10nF 3kV Ceramic Capacitor",
                    Description = "High Voltage",
                    UnitPrice = 2.5M,
                    ImageUrl = string.Format("{0}/images/120x120/3kVCeramicCapacitor.jpg", ImagesPath),
                    QuantityInStock = 140
                },
                QuantityToOrder = 0, SubTotal = 0
            });
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel {
                    Id = 24,
                    Name = "Metal Gear Motor",
                    Description = "12V motor with a 100:1 metal gearbox",
                    UnitPrice = 59.75M,
                    ImageUrl = string.Format("{0}/images/120x120/metalGearmotor.png", ImagesPath),
                    QuantityInStock = 190
                },
                QuantityToOrder = 190,
                SubTotal = 11352.5M
            });
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel
                {
                    Id = 19,
                    Name = "Mini Photocell SEN-09088",
                    Description = "A photoconductive cell. Resistance varies according to the amount of light it is exposed to.",
                    UnitPrice = 1.67M,
                    ImageUrl = string.Format("{0}/images/120x120/photocell.jpeg", ImagesPath),
                    QuantityInStock = 201
                },
                QuantityToOrder = 0,
                SubTotal = 0
            });
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel
                {
                    Id = 29,
                    Name = "PCB Holder with Magnifying Glass",
                    Description = "Magnifying Glass: 90mm",
                    UnitPrice = 12.95M,
                    ImageUrl = string.Format("{0}/images/120x120/pcbHolderWithMag.jpg", ImagesPath),
                    QuantityInStock = 40
                },
                QuantityToOrder = 0,
                SubTotal = 0
            });
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel
                {
                    Id = 18,
                    Name = "Thermistor: NTC 5mm Dia, 100 Ohm",
                    Description = "Made of ceramic material which changes resistance according to temperature. Very reliable.",
                    UnitPrice = 1.35M,
                    ImageUrl = string.Format("{0}/images/120x120/thermistor.png", ImagesPath),
                    QuantityInStock = 230
                },
                QuantityToOrder = 0,
                SubTotal = 0
            });
            return View(model);
        }
    }
}