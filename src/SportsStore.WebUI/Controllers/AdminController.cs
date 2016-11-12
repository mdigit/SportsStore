using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using System.Linq;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository Repository { get; set; }

        public AdminController(IProductRepository repository)
        {
            Repository = repository;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(Repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            var product = Repository.Products.FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }
    }
}