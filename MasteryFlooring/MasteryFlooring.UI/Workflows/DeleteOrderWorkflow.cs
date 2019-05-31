using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasteryFlooring.BLL;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using MasteryFlooring.Models.Responses;

namespace MasteryFlooring.UI.Workflows
{
    public class DeleteOrderWorkflow
    {
        public void Execute()
        {
            IUserIO userIO = new UserIO();
            userIO.Clear();
            FlooringManager manager = FlooringFactoryManager.Create();

            userIO.WriteLine("DELETE ORDER:");
            userIO.WriteLine("");
            userIO.WriteLine(new string('=', 60));
            userIO.WriteLine("");
            DateTime dateTime = HelperMethods.GetDateTime("Enter the order date: ");
            int OrderNumber = HelperMethods.GetIntFromUser("Enter the order number: ");
            EditOrderResponse response = new EditOrderResponse();
            Order order = manager.GetOrderByOrderNumber(dateTime, OrderNumber);
            if (order == null)
            {
                response.Success = false;
                response.Message = "No orders exist with this date.";
            }
            else
            {
                userIO.DisplayOrder(order);
                if (HelperMethods.GetYesNoAnswerFromUser("Would you like to delete this order?"))
                {
                    manager.DeleteOrder(order);
                    response.Success = true;
                    response.Message = "Order successfully deleted.";
                }
            }
        }
    }
}
