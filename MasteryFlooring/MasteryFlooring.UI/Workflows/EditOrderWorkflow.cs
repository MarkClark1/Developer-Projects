using MasteryFlooring.BLL;
using MasteryFlooring.Data;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using MasteryFlooring.Models.Responses;
using System;

namespace MasteryFlooring.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            IUserIO userIO = new UserIO();
            userIO.Clear();
            FlooringManager manager = FlooringFactoryManager.Create();

            userIO.WriteLine("EDIT ORDER:");
            userIO.WriteLine("");
            userIO.WriteLine(new string('=', 60));
            userIO.WriteLine("");
            DateTime dateTime = HelperMethods.GetDateTime("Enter the order date: ");
            int OrderNumber = HelperMethods.GetIntFromUser("Enter the order number: ");
            EditOrderResponse response = new EditOrderResponse();
            Order oldOrder = manager.GetOrderByOrderNumber(dateTime, OrderNumber);
            Order order = oldOrder;
            if (oldOrder == null)
            {
                response.Success = false;
                response.Message = "No orders exist with this date.";
            }
            else
            {
                order.CustomerName = HelperMethods.GetCustomerName($"Previous Name: {oldOrder.CustomerName}\n Please enter the customers name: ", oldOrder.CustomerName);
                Console.WriteLine($"Previous State: {oldOrder.State}");
                order.State = HelperMethods.GetStateTax(TaxRepository.GetTaxes(), oldOrder.State);
                Console.WriteLine($"Previous Product: {oldOrder.ProductType}");
                order.ProductType = HelperMethods.GetProductInformation(ProductRepository.GetProducts());
                order.Area = HelperMethods.GetDecimalFromString($"Previous Area: {oldOrder.Area}\n Enter the Area: ", oldOrder.Area);
                userIO.DisplayOrder(order);
                if (HelperMethods.GetYesNoAnswerFromUser("Would you like to create this order?"))
                {
                    manager.DeleteOrder(oldOrder);
                    manager.AddOrder(order);
                    response.Success = true;
                    response.Message = "Order successfully added.";
                }
            }
        }
    }
}
