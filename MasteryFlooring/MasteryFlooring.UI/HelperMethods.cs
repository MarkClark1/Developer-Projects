using MasteryFlooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasteryFlooring.UI
{
    public static class HelperMethods
    {
        public static bool GetYesNoAnswerFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + "(Y/N)?");
                string input = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter Y/N.");
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter Y/N.");
                        Console.WriteLine("Press any key to continue..");
                        Console.ReadKey();
                    }
                    else if (input == "N") return false;
                    else if (input == "Y") return true;
                }
            }
        }

        public static decimal GetDecimalFromString(string msg, decimal? oldArea = null)
        {
            decimal input;
            Console.WriteLine(msg);
            while (true)
            {
                if (oldArea != null)
                {
                    return (int)oldArea;
                }
                if (!decimal.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                if (input < 100)
                {
                    Console.WriteLine("Area must be equal or greater than 100");
                    continue;
                }
                    break;
            }
            return input;
        }

        public static int GetIntFromUser(string msg, int? oldIndex = null)
        {
            int input;
            Console.WriteLine(msg);
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                if(oldIndex != null)
                {
                    return (int)oldIndex;
                }
                Console.WriteLine("Invalid Input");
                Console.WriteLine(msg);
            }
            return input;
        }

        public static string GetCustomerName(string msg, string oldName = null)
        {
            Console.WriteLine(msg);
            string input = Console.ReadLine();

            while (input == null || input == string.Empty || !input.All(c => Char.IsLetterOrDigit(c) || c == '_' || c == ',' || c == '.' || c == ' '))
            {
                if (!string.IsNullOrEmpty(oldName))
                {
                    return oldName;
                }
                Console.WriteLine("Invalid Input.");
                Console.WriteLine(msg);
                input = Console.ReadLine();
            }
            return input;
        }

        public static string GetStateTax(List<Tax> taxes, string oldState = null)
        {
            string state;
            foreach (Tax tax in taxes)
            {
                if (tax != null)
                {
                    Console.WriteLine("  " + (taxes.IndexOf(tax) + 1) + ". " + tax.StateName);
                }                
            }            
            while (true)
            {
                try
                {
                    int oldIndex = taxes.IndexOf(taxes.Where(x => x.StateAbbreviation == oldState).FirstOrDefault()) + 1;
                    int input = GetIntFromUser("Type the number of the state.", oldIndex) - 1;
                    state = taxes[input].StateAbbreviation;
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return state;
        }

        public static string GetProductInformation(List<Product> products, string oldProduct = null)
        {
            string productinfo;
            foreach (Product product in products)
            {
                if(product != null)
                {
                    Console.WriteLine("  " + (products.IndexOf(product) + 1) + ". " + product.ProductType);
                }                
            }
            while (true)
            {
                try
                {
                    int oldIndex = products.IndexOf(products.Where(x => x.ProductType == oldProduct).FirstOrDefault()) + 1;
                    int input = GetIntFromUser("Enter the type of product you will be using.") - 1;
                    productinfo = products[input].ProductType;
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid product type.");
                }
            }
            return productinfo;
        }

        public static DateTime GetDateTime(string msg)
        {
            while (true)
            {
                Console.WriteLine(msg);
                DateTime input;
                if (DateTime.TryParse(Console.ReadLine(), out input))
                {
                    if (input > DateTime.Now)
                    {
                        return input;
                    }
                    Console.WriteLine("Date for order must be beyond today's date.");
                }
                Console.WriteLine("Invalid date entered.");
            }
        }
    }
}
