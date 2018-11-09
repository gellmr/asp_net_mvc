using System.Linq;
using System.Web.Mvc;
using gellmvc.Domain.Abstract;
using gellmvc.Domain.Entities;
using gellmvc.Models;

namespace gellmvc.Controllers
{
  public class CartController : Controller
  {
    private IProductRepository repository;

    public CartController(IProductRepository repo) {
      repository = repo;
    }

    public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
    {
      Product product = repository.Products.FirstOrDefault(p => p.Id == Id);

      if (product != null) {
        cart.AddItem(product, 1);
      }
      return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToRouteResult RemoveFromCart(Cart cart, int Id, string returnUrl)
    {
      Product product = repository.Products.FirstOrDefault(p => p.Id == Id);

      if (product != null)
      {
        cart.RemoveLine(product);
      }
      return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToRouteResult ClearCart(Cart cart)
    {
      cart.Clear();
      return RedirectToAction("Index");
    }


    public ViewResult Index(Cart cart, string returnUrl)
    {
      return View(new CartIndexViewModel
      {
        Cart = cart,
        ReturnUrl = returnUrl
      });
    }

    [HttpPut]
    public HttpStatusCodeResult PutUpdate(Cart cart, CartUpdate cartUpdate)
    {
      // Check if there are sufficient quantity in stock.
      Product product = repository.Products.FirstOrDefault(p => p.Id == cartUpdate.ProductId);

      if (product.QuantityInStock >= cartUpdate.NewQty)
      {
        // We have enough stock...
        if (cartUpdate.NewQty == 0)
        {
          // User set qty to zero.
          cart.RemoveLine(product);
          return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK, "Removed line: " + product.Name);
        }
      }

      return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
    }
  }
}