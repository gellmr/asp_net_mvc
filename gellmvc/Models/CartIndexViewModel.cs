using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gellmvc.Domain.Entities;

namespace gellmvc.Models
{
  public class CartIndexViewModel {
    public Cart Cart { get; set; }
    public string ReturnUrl { get; set; }
  }
}