using System.Linq;
using System.Web.Mvc;
using gellmvc.Domain.Abstract;
using gellmvc.Domain.Entities;
using gellmvc.Models;
using System.Collections.Generic;

namespace gellmvc.Controllers
{
  public class StoreController : Controller
  {
    private IProductRepository repository;
    public int PageSize = 5;

    public StoreController(IProductRepository productRepository){
      this.repository = productRepository;
    }

    // GET: /Store/Index
    public ViewResult Index(Cart cart, int page = 1)
    {
      List<ProductLine> productLines = new List<ProductLine>();

      IEnumerable<Product> allProducts = repository.Products
        .OrderBy(p => p.Id)
        .Skip((page - 1) * PageSize)
        .Take(PageSize);
      
      foreach (Product product in allProducts)
      {
        int qty = cart.GetQuantity(product);
        productLines.Add(new ProductLine
        {
          Product = product,
          QtyInCart = qty,
          Subtotal = qty * product.UnitPrice
        });
      }
      
      StoreListViewModel model = new StoreListViewModel
      {
        ProductLines = productLines,
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = PageSize,
          TotalItems = repository.Products.Count()
        },
        Cart = cart
      };
      return View(model);
    }
  }
}