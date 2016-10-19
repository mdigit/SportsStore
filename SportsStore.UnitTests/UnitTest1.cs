using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate1()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" }
                });
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            var result = (IEnumerable<Product>)controller.List(null, 2).Model;
            // Assert
            var prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Paginate2()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" }
                });
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            var result = (ProductsListViewModel)controller.List(null, 2).Model;
            // Assert
            var prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange: define HTML helper - we need to do this in order to apply the extension method.
            HtmlHelper htmlHelper = null;
            // Arrange: create PaginInfo data
            var pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            // Arragne: set up the delegate using a lambda expression
            Func<int, string> pageUriDelegate = i => "Page" + i;

            // Act
            var result = htmlHelper.PageLinks(pagingInfo, pageUriDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1<</a><a class=""btn btn-default btn-primary selected"" href=""Page2"">2<</a><a class=""btn btn-default"" href=""Page3"">3<</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arragne
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Catagory = "Cat1" },
                    new Product { ProductID = 2, Name = "P2", Catagory = "Cat2" },
                    new Product { ProductID = 3, Name = "P3", Catagory = "Cat1" },
                    new Product { ProductID = 4, Name = "P4", Catagory = "Cat2" },
                    new Product { ProductID = 5, Name = "P5", Catagory = "Cat3" }
                });
            // Arrange - create a controller and make the page size 3 items
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Action
            var result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();
            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Catagory == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Catagory == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arragne
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Catagory = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Catagory = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Catagory = "Plums" },
                    new Product { ProductID = 4, Name = "P4", Catagory = "Oranges" }
                });
            // Arrange - create the controller
            var controller = new NavController(mock.Object);
            
            // Act = get the set of categories
            var result = ((IEnumerable<Product>)controller.Menu().Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID = 1, Name = "P1", Catagory = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Catagory = "Oranges" }
                });

            // Arrange - create the controller
            var controller = new NavController(mock.Object);
            // Arrange - define the category to selected
            var categoryToSelected = "Apples";

            // Action
            var result = controller.Menu(categoryToSelected).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelected, result);
        }
    }
}
