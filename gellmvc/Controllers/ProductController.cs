﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gellmvc.Domain.Abstract;
using gellmvc.Domain.Entities;
using gellmvc.Models;

namespace gellmvc.Controllers
{
  public class ProductController : Controller
  {
    private IProductRepository repository;
    public int PageSize = 4;

    // constructor with DI
    public ProductController(IProductRepository productRepository)
    {
        this.repository = productRepository;
    }

    public ViewResult List(int page = 1)
    {
      ProductsListViewModel model = new ProductsListViewModel
      {
        Products = repository.Products
        .OrderBy(p => p.Id)
        .Skip((page - 1) * PageSize)
        .Take(PageSize),
        PagingInfo = new PagingInfo {
          CurrentPage = page,
          ItemsPerPage = PageSize,
          TotalItems = repository.Products.Count()
        }
      };
      return View(model);
    }
  }
}