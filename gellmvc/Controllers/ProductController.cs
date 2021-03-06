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
    
    public ProductController(IProductRepository productRepository){
        this.repository = productRepository;
    }

    public ViewResult List(int page = 1)
    {
      ProductsListViewModel model = new ProductsListViewModel
      {
        Products = repository.Products.OrderBy(p => p.Id)
          .Skip((page - 1) * PageSize)
          .Take(PageSize),
          Pager = new Pager(repository.Products.Count(), page, PageSize)
      };
      return View(model);
    }
  }
}