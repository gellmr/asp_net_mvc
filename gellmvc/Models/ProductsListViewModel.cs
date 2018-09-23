﻿using System.Collections.Generic;
using gellmvc.Domain.Entities;
using gellmvc.Helpers;

namespace gellmvc.Models
{
  public class ProductsListViewModel
  {
    public IEnumerable<Product> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}