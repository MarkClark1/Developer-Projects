using MasteryFlooring.BLL;
using MasteryFlooring.Models;
using MasteryFlooring.Models.Interfaces;
using MasteryFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.UI.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            IUserIO userIO = new UserIO();
            DisplayOrderResponse response = new DisplayOrderResponse();
            FlooringManager manager = FlooringFactoryManager.Create();

            userIO.Clear();
            userIO.WriteLine("DISPLAY ORDER: ");
            userIO.WriteLine("");
            userIO.WriteLine(new string('=', 60));
            userIO.WriteLine("");
            List<Order> orders = manager.GetOrdersByDate(HelperMethods.GetDateTime("Enter an order date: "));

            if (orders == null || orders.Count == 0)
            {
                response.Success = false;
                response.Message = ("No order exists with that date.");
            }
            else
            {
                response.Success = true;
            }
            if (response.Success)
            {
                userIO.DisplayOrders(orders);
            }
            else
            {
                userIO.WriteLine("An error has occured: ");
                userIO.WriteLine(response.Message);
            }
            userIO.WriteLine("Press any key to continue.");
            userIO.ReadKey();
        }
    }
}
