using System.Collections.Generic;
using gellmvc.Domain.Entities;

namespace gellmvc.Models
{
  public class ProductsListViewModel
  {
    public IEnumerable<Product> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string CurrentCategory { get; set; }
  }
}