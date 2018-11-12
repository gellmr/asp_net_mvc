﻿using gellmvc.Domain.Abstract;
using gellmvc.Domain.Entities;
using gellmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static gellmvc.Domain.Entities.Cart;

namespace gellmvc.Controllers
{
  public abstract class CartBaseController : Controller
  {
    protected IProductRepository repository;

    public CartBaseController(IProductRepository repo){
      repository = repo;
    }
    
    protected CartIndexViewModel LookUpProducts(Cart cart)
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

    // Set some headers on our http response
    protected void cartResponseHeaders(Cart cart, string cartResult, int resultCartQty, decimal resultSubTot, string message)
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