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
        private string SiteImages;

        public StoreController(){
            SiteImages = ApplicationHelper.SiteImagesFolderPath();
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
                    ImageUrl = string.Format("{0}/120x120/3kVCeramicCapacitor.jpg", SiteImages),
                    QuantityInStock = 140
                },
                QuantityToOrder = 0, SubTotal = 0
            });
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel
                {
                    Id = 2,
                    Name = "270pF 50Volt Ceramic Capacitor",
                    Description = "Lead Spacing: 5mm (Pack of 2)",
                    UnitPrice = 0.32M,
                    ImageUrl = string.Format("{0}/120x120/270pF_50_VoltCeramicCapacitor.jpg", SiteImages),
                    QuantityInStock = 200
                },
                QuantityToOrder = 0,
                SubTotal = 0
            });
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel
                {
                    Id = 3,
                    Name = "500 Ohm 24mm Potentiometer",
                    Description = "Full size - 24mm diameter. Power rating is 0.5W maximum.",
                    UnitPrice = 2.25M,
                    ImageUrl = string.Format("{0}/120x120/pot.png", SiteImages),
                    QuantityInStock = 230
                },
                QuantityToOrder = 0,
                SubTotal = 0
            });

            /*
            model.Order.Products.Add(new OrderedProductViewModel
            {
                Product = new ProductViewModel {
                    Id = 24,
                    Name = "Metal Gear Motor",
                    Description = "12V motor with a 100:1 metal gearbox",
                    UnitPrice = 59.75M,
                    ImageUrl = string.Format("{0}/120x120/metalGearmotor.png", SiteImages),
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
                    ImageUrl = string.Format("{0}/120x120/photocell.jpeg", SiteImages),
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
                    ImageUrl = string.Format("{0}/120x120/pcbHolderWithMag.jpg", SiteImages),
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
                    ImageUrl = string.Format("{0}/120x120/thermistor.png", SiteImages),
                    QuantityInStock = 230
                },
                QuantityToOrder = 0,
                SubTotal = 0
            });*/
            return View(model);
        }
    }
}