using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_net_mvc.ViewModels.Store;
using System.Configuration;

namespace asp_net_mvc.Controllers
{
    public class StoreController : Controller
    {
        private string ImagesPath;

        public StoreController(){
            ImagesPath = ConfigurationManager.AppSettings["localImagesFilePath"].ToString();
        }

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
                imageUrl = string.Format("{0}/images/120x120/3kVCeramicCapacitor.jpg", ImagesPath),
                quantityInStock = 140
            });

            model.products.Add(new ProductViewModel
            {
                id = 24,
                name = "Metal Gear Motor",
                description = "12V motor with a 100:1 metal gearbox",
                unitPrice = 59.75M,
                imageUrl = string.Format("{0}/images/120x120/metalGearmotor.png", ImagesPath),
                quantityInStock = 190
            });
            model.products.Add(new ProductViewModel
            {
                id = 19,
                name = "Mini Photocell SEN-09088",
                description = "A photoconductive cell. Resistance varies according to the amount of light it is exposed to.",
                unitPrice = 1.67M,
                imageUrl = string.Format("{0}/images/120x120/photocell.jpeg", ImagesPath),
                quantityInStock = 201
            });
            model.products.Add(new ProductViewModel
            {
                id = 29,
                name = "PCB Holder with Magnifying Glass",
                description = "Magnifying Glass: 90mm",
                unitPrice = 12.95M,
                imageUrl = string.Format("{0}/images/120x120/pcbHolderWithMag.jpg", ImagesPath),
                quantityInStock = 40
            });
            model.products.Add(new ProductViewModel
            {
                id = 18,
                name = "Thermistor: NTC 5mm Dia, 100 Ohm",
                description = "Made of ceramic material which changes resistance according to temperature. Very reliable.",
                unitPrice = 1.35M,
                imageUrl = string.Format("{0}/images/120x120/thermistor.png", ImagesPath),
                quantityInStock = 230
            });

            return View(model);
        }
    }
}