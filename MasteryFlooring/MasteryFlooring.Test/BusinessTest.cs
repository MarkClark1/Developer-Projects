using MasteryFlooring.BLL.Services;
using MasteryFlooring.Data;
using MasteryFlooring.Data.Repositories;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Responses;
using NUnit.Framework;
using System;

namespace MasteryFlooring.Test
{
    [TestFixture]
    public class BusinessTest
    {
       // [TestCase(0, 1)]
        [TestCase(5, 5)]
        [TestCase(3, 3)]
        public void CheckOrderNumber(int orderNumber, int expectedOrderNumber)
        {
            Order order = new Order()
            {
                OrderDate = DateTime.Parse("01/20/2020"),
                Area = 100,
                ProductType = "Carpet",
                State = "PA",
                CustomerName = "Anthony",
                OrderNumber = orderNumber
            };

            TestRepository orderRepository = new TestRepository();
            FlooringServices services = new FlooringServices(orderRepository);
            AddOrderResponse response = services.Add(order);

            Assert.AreEqual(expectedOrderNumber, response.order.OrderNumber);
        }

        [TestCase("Anthony", "PA", "Carpet", 220, 2.25, 2.10, true)]
        [TestCase("^Anthony", "PA", "Carpet", 220, 2.25, 2.10, false)] //wrong name input
        [TestCase("Anthony", "ND", "Carpet", 220, 2.25, 2.10, false)] //invalid state
        [TestCase("Anthony", "PA", "Asphalt", 220, 2.25, 2.10, false)] //invalid product
        [TestCase("Anthony", "PA", "Carpet", 20, 2.25, 2.10, false)] //invalid area

        public void AddOrderTest(string name, string state, string product, decimal area, decimal CostPerSquareFoot, decimal laborCostPerSquareFoot, bool expectedResult)
        {

            Order order = new Order()
            {
                CustomerName = name,
                State = state,
                ProductType = product,
                Area = area,
                CostPerSquareFoot = CostPerSquareFoot,
                LaborCostPerSquareFoot = laborCostPerSquareFoot
            };

            AddOrderResponse response = new AddOrderResponse();
            TestRepository orderRepository = new TestRepository();
            FlooringServices services = new FlooringServices(orderRepository);
            response = services.Add(order);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
