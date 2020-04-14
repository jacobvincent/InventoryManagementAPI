using InventoryManagement.Controllers;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace InventoryManagementTest
{
    public class InventoryControllerTest
    {
        [Fact]
        public async Task GetProductLists_Success()
        {
            var productMock = new Mock<IInventoryServices>();
            productMock.Setup(prodList => prodList.GetProductLists(It.IsAny<string>())).ReturnsAsync(GetProductTestSessions());
            var controller = new InventoryController(productMock.Object);

            var result = await controller.GetProductLists();
            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult);
            var items = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, items.Count);

        }

        [Fact]
        public async Task GetExchangeRates_Success()
        {
            var exchangeMock = new Mock<IInventoryServices>();
            exchangeMock.Setup(xList => xList.GetExchangeRates(It.IsAny<string>())).ReturnsAsync(GetExchangeTestSessions());
            var controller = new InventoryController(exchangeMock.Object);

            var result = await controller.GetExchangeRates();
            var okResult = result.Result as OkObjectResult;

            Assert.NotNull(okResult);
            var items = Assert.IsType<List<FxRate>>(okResult.Value);
            Assert.Equal(2, items.Count);

        }


        private List<Product> GetProductTestSessions()
        {
            var sessions = new List<Product>();
            sessions.Add(new Product()
            {
                ProductId = "100AC-001",
                Description = "Test One clouds are clouds which have flat bases and are often described as \"puffy\", \"cotton-like\" or \"fluffy\" in appearance.",
                Name = "Test One",
                UnitPrice = 10.98,
                MaximumQuantity=null
            });
            sessions.Add(new Product()
            {
                ProductId = "100AC-002",
                Description = "Test Two clouds hang low in the sky as a flat, featureless, uniform layer of grayish cloud. It resembles fog that hugs the horizon (instead of the ground).",
                Name = "Test Two",
                UnitPrice = 12.84,
                MaximumQuantity = null
            });
            return sessions;
        }

        private List<FxRate> GetExchangeTestSessions()
        {
            var sessions = new List<FxRate>();
            sessions.Add(new FxRate()
            {
                SourceCurrency = "AUD",
                TargetCurrency = "USD",
                Rate = .75
            });
            sessions.Add(new FxRate()
            {
                SourceCurrency = "USD",
                TargetCurrency = "AUD",
                Rate = 1.33
            });
            return sessions;
        }
    }
}
