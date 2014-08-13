﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IProductsRepository> monk = new Mock<IProductsRepository>();
            monk.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product {ProductID=1, Name="P1"},
                new Product {ProductID=2, Name="P2"},
                new Product {ProductID=3, Name="P3"},
                new Product {ProductID=4, Name="P4"},
                new Product {ProductID=5, Name="P5"}

            });

            // create a controller and make the page size 3 items
            ProductController controller = new ProductController(monk.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange 
            
            // define an HTML helper in order to apply the extension method
            HtmlHelper myHelper = null;
            
            // create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            
            // set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Sent_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductsRepository> monk = new Mock<IProductsRepository>();
            monk.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product {ProductID=1, Name="P1"},
                new Product {ProductID=2, Name="P2"},
                new Product {ProductID=3, Name="P3"},
                new Product {ProductID=4, Name="P4"},
                new Product {ProductID=5, Name="P5"}

            });

            // Arrange
            ProductController controller = new ProductController(monk.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category= "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category= "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category= "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category= "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category= "Cat3"},
            });

            // Arrange
            // - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");

        }
    }
}
