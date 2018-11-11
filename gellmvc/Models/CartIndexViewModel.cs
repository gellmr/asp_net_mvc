using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gellmvc.Domain.Entities;

namespace gellmvc.Models
{
  public class CartIndexViewModel
  {
    public IEnumerable<ProductLine> ProductLines { get; set; }
    public Cart Cart { get; set; }
  }

  public class CartUpdate
  {
    public int ProductId { get; set; }
    public int NewQty { get; set; }
  }
}