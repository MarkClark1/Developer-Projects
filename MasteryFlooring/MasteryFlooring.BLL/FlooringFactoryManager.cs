using MasteryFlooring.BLL.Services;
using MasteryFlooring.Data;
using MasteryFlooring.Data.Repositories;
using System.Configuration;

namespace MasteryFlooring.BLL
{
    public class FlooringFactoryManager
    {
        public static FlooringManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Project":
                    return new FlooringManager(new FlooringServices(new OrderRepository()));
                case "Test":
                    return new FlooringManager(new FlooringServices(new TestRepository()));
                default:
                    throw new System.Exception("Mode value in app is not set to a valid mode.");
            }
        }
    }
}
