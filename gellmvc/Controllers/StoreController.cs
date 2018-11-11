using System.Linq;
using System.Web.Mvc;
using gellmvc.Domain.Abstract;
using gellmvc.Domain.Entities;
using gellmvc.Models;
using System.Collections.Generic;

namespace gellmvc.Controllers
{
  public class StoreController : Controller
  {
    private IProductRepository repository;
    public int PageSize = 5;

    public StoreController(IProductRepository productRepository){
      this.repository = productRepository;
    }

    private StoreListViewModel GetPageOfProducts(Cart cart, int page = 1, string searchString = "")
    {
      searchString = searchString.ToLower();

      List<ProductLine> productLines = new List<ProductLine>();
      IEnumerable<Product> pageOfProducts;

      // Get a page of products from the repository
      if (searchString.Length == 0)
      {
        // no search string
        pageOfProducts = repository.Products
          .OrderBy(p => p.Id)
          .Skip((page - 1) * PageSize)
          .Take(PageSize);
      }
      else
      {
        // search string
        pageOfProducts = repository.Products
          .Where(p => p.Name.ToLower().Contains(searchString) || p.Description.ToLower().Contains(searchString))
          .OrderBy(p => p.Id)
          .Skip((page - 1) * PageSize)
          .Take(PageSize);
      }

      // Update each line according to how many are currently in the cart.
      foreach (Product product in pageOfProducts)
      {
        int qty = cart.GetQuantity(product);
        productLines.Add(new ProductLine
        {
          Product = product,
          QtyInCart = qty,
          Subtotal = qty * product.UnitPrice
        });
      }

      // Construct the view model
      StoreListViewModel viewModel = new StoreListViewModel
      {
        ProductLines = productLines,
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = PageSize,
          TotalItems = repository.Products.Count()
        },
        Cart = cart,
        SearchString = searchString
      };

      return viewModel;
    }

    // GET: /Store/Search
    public ViewResult Search(Cart cart, int page = 1, string searchString = "")
    {
      StoreListViewModel model = GetPageOfProducts(cart, page, searchString);
      return View("Index", model);
    }
  }
}