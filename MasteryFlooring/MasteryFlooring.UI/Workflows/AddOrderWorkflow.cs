using MasteryFlooring.BLL;
using MasteryFlooring.Data;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using MasteryFlooring.Models.Responses;
using System;

namespace MasteryFlooring.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            IUserIO userIO = new UserIO();
            userIO.Clear();
            FlooringManager manager = FlooringFactoryManager.Create();
            Order order = new Order();

            userIO.WriteLine("CREATE NEW ORDER: ");
            userIO.WriteLine("");
            userIO.WriteLine(new string('=', 60));
            userIO.WriteLine("");
            order.OrderDate = HelperMethods.GetDateTime("Please enter the date of order: ");
            order.CustomerName = HelperMethods.GetCustomerName("Please enter the customers name: ");
            order.State = HelperMethods.GetStateTax(TaxRepository.GetTaxes());
            order.ProductType = HelperMethods.GetProductInformation(ProductRepository.GetProducts());
            order.Area = HelperMethods.GetDecimalFromString("Enter the Area: ");

            order = manager.CalculateOrder(order);
            userIO.DisplayOrder(order);
            
            if (HelperMethods.GetYesNoAnswerFromUser("Would you like to create this order?"))
            {
                userIO.WriteLine("Order has succesfully been created.");
                AddOrderResponse response = manager.AddOrder(order);
            }
            else
            {
                userIO.WriteLine("Order was not created.");
            }
        }
    }
}
