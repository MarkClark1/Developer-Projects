using MasteryFlooring.Data;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using MasteryFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasteryFlooring.BLL.Services
{
    public class FlooringServices : IServices
    {
        private IOrderRepository _or;

        public FlooringServices(IOrderRepository or)
        {
            _or = or;
        }

        public AddOrderResponse Add(Order order)
        {
            AddOrderResponse response = new AddOrderResponse();
            if (!ValidateOrder(order))
            {
                response.Success = false;
                return response;
            }
            response.order = _or.Add(order.OrderDate, order);            
            response.Success = true;
            response.Message = ("Order successfully added to the repository.");
            return response;
        }

        public bool ValidateOrder(Order order)
        {
            List<Tax> taxes = GetTaxList();
            bool verifyTax = false;
            for (int i = 0; i < taxes.Count; i++)
            {
                if (order.State == taxes[i].StateAbbreviation)
                {
                    verifyTax = true;
                    break;
                }
            }
            if (verifyTax == false)  
            {
                return false;
            }

            List<Product> products = GetProductsList();
            bool verifyProduct = false;
            for (int i = 0; i < products.Count; i++)
            {
                if (order.ProductType == products[i].ProductType)
                {
                    verifyProduct = true;
                    break;
                }
            }
            if (verifyProduct == false)
            {
                return false;
            }

            if (order.Area < 100)
            {
                return false;
            }

            if (!order.CustomerName.All(c => Char.IsLetterOrDigit(c) || c == '_' || c == ',' || c == '.' || c == ' '))
            {
                return false;
            }
            return true;
        }

        public Order GetOrderByOrderNumber(DateTime dateTime, int OrderNumber)
        {
            List<Order> orders = _or.GetOrders(dateTime);
            foreach (var o in orders)
            {
                if (o.OrderNumber == OrderNumber)
                {
                    return o;
                }
            }
            return null;
        }

        public List<Order> GetOrdersByDate(DateTime dateTime)
        {
            return _or.GetOrders(dateTime);
        }

        public List<Product> GetProductsList()
        {
            return ProductRepository.GetProducts();
        }

        public List<Tax> GetTaxList()
        {
            return TaxRepository.GetTaxes();
        }

        public DeleteOrderResponse Delete(Order order)
        {
            DeleteOrderResponse response = new DeleteOrderResponse();
            _or.Delete(order);
            response.Success = true;
            return response;
        }

        public Order CalculateOrder(Order order)
        {
            List<Product> products = ProductRepository.GetProducts();
            foreach (var product in products)
            {
                if (order.ProductType == product.ProductType)
                {
                    order.CostPerSquareFoot = product.CostPerSquareFoot;
                    order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                    break;
                }
            }
            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;

            List<Tax> taxes = TaxRepository.GetTaxes();
            foreach (var tax in taxes)
            {
                if (tax.StateAbbreviation == order.State)
                {
                    order.Tax = (order.MaterialCost + order.LaborCost) * (tax.TaxRate / 100);
                    order.TaxRate = tax.TaxRate;
                }
            }
            order.Total = order.MaterialCost + order.LaborCost + order.Tax;
            order.Area = decimal.Round(order.Area, 2);
            order.CostPerSquareFoot = decimal.Round(order.CostPerSquareFoot, 2);
            order.LaborCostPerSquareFoot = decimal.Round(order.LaborCostPerSquareFoot, 2);
            order.MaterialCost = decimal.Round(order.MaterialCost, 2);
            order.LaborCost = decimal.Round(order.LaborCost, 2);
            order.Total = decimal.Round(order.Total, 2);
            return order;
        }
    }
}
