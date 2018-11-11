using System.Linq;
using System.Web.Mvc;
using gellmvc.Domain.Abstract;
using gellmvc.Domain.Entities;
using gellmvc.Models;
using System.Collections.Generic;
using static gellmvc.Domain.Entities.Cart;

namespace gellmvc.Controllers
{
  public class CartController : Controller
  {
    private IProductRepository repository;

    public CartController(IProductRepository repo) {
      repository = repo;
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

    private CartIndexViewModel LookUpProducts(Cart cart)
    {
      List<ProductLine> productLines = new List<ProductLine>();
      
      foreach (CartLine cartLine in cart.Lines)
      {
        // Look up the product
        Product product = repository.Products.FirstOrDefault(p => p.Id == cartLine.Product.Id);
        int qty = cartLine.Quantity;
        productLines.Add(new ProductLine
        {
          Product = product,
          QtyInCart = qty,
          Subtotal = qty * product.UnitPrice
        });
      }

      // Construct the view model
      CartIndexViewModel viewModel = new CartIndexViewModel
      {
        ProductLines = productLines,
        Cart = cart
      };

      return viewModel;
    }

    public ViewResult Index(Cart cart)
    {
      return View(LookUpProducts(cart));
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
          cartResponseHeaders(cart, "updated-qty", 0, 0, "Updated cart - set qty to zero.");
          return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        else
        {
          if (cartUpdate.NewQty > 0)
          {
            // User set qty to positive integer. Update cart quantity.
            Cart.CartLine line = cart.SetLineQuantity(product, cartUpdate.NewQty);
            cartResponseHeaders(cart, "updated-qty", line.Quantity, line.SubTotal, "Updated cart.");
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
          }
          else
          {
            // User set qty to negative integer. Remove from cart.
            cart.RemoveLine(product);
            cartResponseHeaders(cart, "removed-from-cart", 0, 0, "Removed item from cart.");
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
          }
        }
      }
      else
      {
        // User wants more than we have available. Set to max available.
        Cart.CartLine line = cart.SetLineQuantity(product, product.QuantityInStock);
        Response.AddHeader("max", product.QuantityInStock.ToString());
        cartResponseHeaders(cart,
          "set-to-max",
          product.QuantityInStock,
          product.UnitPrice * product.QuantityInStock,
          "Only " + product.QuantityInStock + " items available!"
        );
        return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
      }
    }

    // Set some headers on our http response
    private void cartResponseHeaders(Cart cart, string cartResult, int resultCartQty, decimal resultSubTot, string message)
    {
      Response.AddHeader("result", cartResult.ToString());
      Response.AddHeader("resultCartQty", resultCartQty.ToString());
      Response.AddHeader("resultSubTot", resultSubTot.ToString());
      Response.AddHeader("message", message);

      Response.AddHeader("resultGrandTot", cart.GrandTotal().ToString());
      Response.AddHeader("cartTotalItems", cart.TotalItemsInCart().ToString());
      Response.AddHeader("cartTotalLines", cart.Lines.Count().ToString());
    }
  }
}