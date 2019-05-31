using MasteryFlooring.Models.Responses;
using MasteryFlooring.UI.Workflows;
using System;

namespace MasteryFlooring.UI
{
    public class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.WriteLine(new string('=', 60));
                Console.WriteLine("");
                Console.WriteLine("                    FLOORING MENU");
                Console.WriteLine("");
                Console.WriteLine(new string('=',60));
                Console.WriteLine("");
                Console.WriteLine("  1. Display Order.");
                Console.WriteLine("  2. Add an Order.");
                Console.WriteLine("  3. Edit an Order.");
                Console.WriteLine("  4. Remove an Order.");
                Console.WriteLine("  5. Quit.");
                Console.WriteLine("");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine("");
                Console.WriteLine("  Please Enter a Selection: ");
                string userinput = Console.ReadLine();

                switch (userinput)
                {
                    case "1":
                        DisplayOrdersWorkflow displayWorkflow = new DisplayOrdersWorkflow();
                        displayWorkflow.Execute();
                            break;
                    case "2":
                        AddOrderWorkflow addWorkflow = new AddOrderWorkflow();
                        addWorkflow.Execute();
                            break;
                    case "3":
                        EditOrderWorkflow editWorkflow = new EditOrderWorkflow();
                        editWorkflow.Execute();
                            break;
                    case "4":
                        DeleteOrderWorkflow deleteWorkflow = new DeleteOrderWorkflow();
                        deleteWorkflow.Execute();
                            break;
                    case "5":
                        Console.WriteLine("System End.");
                        return;
                }
            }
        }
    }
}
