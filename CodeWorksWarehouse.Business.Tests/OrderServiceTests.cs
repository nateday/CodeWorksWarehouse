using CodeWorksWarehouse.Business.Services;
using CodeWorksWarehouse.Common.Entities;
using CodeWorksWarehouse.Common.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace CodeWorksWarehouse.Business.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        [TestMethod]
        public void Unresolved_FillOrders_Can_Be_Retrieved()
        {
            //arrange
            var orders = new List<Order>();

            var order1 = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-1),
                ProcessedAt = null,
                RemoveStock = false,
                Stock = 10
            };

            var order2 = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-1),
                ProcessedAt = null,
                RemoveStock = false,
                Stock = 10
            };

            orders.Add(order1);
            orders.Add(order2);

            var mockOrdersRepository = Substitute.For<IOrdersRepository>();
            mockOrdersRepository.GetUnProcessedOrders().Returns(orders);

            var orderService = new OrderService(mockOrdersRepository);

            //act
            var expectedOrders = orderService.GetUnprocessedOrders();

            //assert
            Assert.IsNotNull(expectedOrders);
        }

        [TestMethod]
        public void Unresolved_FillOrder_Can_Be_Processed()
        {
            //arrange
            var userOrder = new Order()
            {
                Id = Guid.NewGuid()
            };

            var order = new Order()
            {
                Id = userOrder.Id,
                ProcessedAt = null
            };

            var mockOrdersRepository = Substitute.For<IOrdersRepository>();
            mockOrdersRepository.GetOrderById(userOrder.Id).Returns(order);

            var orderService = new OrderService(mockOrdersRepository);

            //act
            var expectedOrder = orderService.ProcessOrder(userOrder.Id);

            //assert
            Assert.IsNotNull(expectedOrder.ProcessedAt, "ProcessedAt was not set.");
            mockOrdersRepository.Received(1).GetOrderById(userOrder.Id);
            mockOrdersRepository.ReceivedWithAnyArgs(1).UpdateOrder(order);
        }

        [TestMethod]
        public void Processed_FillOrder_Cannont_Be_Modified()
        {
            //arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-2),
                ProcessedAt = DateTimeOffset.Now.AddDays(-1),
                RemoveStock = false,
                Stock = 10
            };

            var mockOrdersRepository = Substitute.For<IOrdersRepository>();

            var orderService = new OrderService(mockOrdersRepository);

            //act
            try
            {
                orderService.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                //assert
                Assert.AreEqual("A processed order cannot be updated.", ex.Message);
                return;
            }

            Assert.Fail("Should not get here");
        }

        [TestMethod]
        public void Processed_FillOrder_Cannont_Be_Processed()
        {
            //arrange
            var userOrder = new Order()
            {
                Id = Guid.NewGuid()
            };

            var order = new Order
            {
                Id = userOrder.Id,
                ProductId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-2),
                ProcessedAt = DateTimeOffset.Now.AddDays(-1),
                RemoveStock = false,
                Stock = 10
            };

            var mockOrdersRepository = Substitute.For<IOrdersRepository>();
            mockOrdersRepository.GetOrderById(userOrder.Id).Returns(order);

            var orderService = new OrderService(mockOrdersRepository);

            //act
            try
            {
                orderService.ProcessOrder(order.Id);
            }
            catch (Exception ex)
            {
                //assert
                Assert.AreEqual("Order has already been processed.", ex.Message);
                return;
            }

            Assert.Fail("Should not get here");
        }

        [TestMethod]
        public void Existing_FillOrder_Cannot_Be_Created()
        {
            //arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now.AddDays(-2),
                ProcessedAt = DateTimeOffset.Now.AddDays(-1),
                RemoveStock = false,
                Stock = 10
            };

            var mockOrdersRepository = Substitute.For<IOrdersRepository>();

            var orderService = new OrderService(mockOrdersRepository);

            //act
            try
            {
                orderService.CreateOrder(order);
            }
            catch (Exception ex)
            {
                //assert
                Assert.AreEqual("Order already exists.", ex.Message);
                return;
            }

            Assert.Fail("Should not get here");
        }
    }
}
