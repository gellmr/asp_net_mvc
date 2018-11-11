using System.Collections.Generic;
using gellmvc.Domain.Entities;
using gellmvc.Helpers;

namespace gellmvc.Models
{
  public class StoreListViewModel
  {
    public IEnumerable<ProductLine> ProductLines { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public Cart Cart { get; set; }
    public string SearchString { get; set; }
  }
}