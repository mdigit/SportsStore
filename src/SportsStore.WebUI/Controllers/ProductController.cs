using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using System.Linq;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository Repository { get; set; }
        public int PageSize { get; set; } = 4;

        public ProductController(IProductRepository repository)
        {
            Repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var model = new ProductsListViewModel
            {
                Products = Repository.Products
                .Where(p => category == null || p.Catagory == category)
                 .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? Repository.Products.Count() : Repository.Products.Where(x => x.Catagory == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}