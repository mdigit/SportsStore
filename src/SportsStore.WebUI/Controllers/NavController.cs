using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using System.Linq;
using System.Collections.Generic;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository Repository { get; set; }

        public NavController(IProductRepository repository)
        {
            Repository = repository;
        }

        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {
            ViewBag.SelectedCategory = category;
            var categories = Repository.Products
                .Select(x => x.Catagory)
                .Distinct()
                .OrderBy(x => x);
            var viewName = horizontalLayout ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
    }
}