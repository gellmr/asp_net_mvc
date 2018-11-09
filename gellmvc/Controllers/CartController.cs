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

    public ViewResult Index(Cart cart, string returnUrl)
    {
      return View(new CartIndexViewModel {
        Cart = cart,
        ReturnUrl = returnUrl
      });
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

    [HttpPost]
    public HttpStatusCodeResult PutUpdate(CartUpdate cartUpdate)
    {
      return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
    }
  }
}