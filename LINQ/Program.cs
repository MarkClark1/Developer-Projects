using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();
            //Exercise1();
            //Exercise2();
            //Exercise3();
            //Exercise4();
            //Exercise5();
            //Exercise6();
            //Exercise7();
            //Exercise8();
            //Exercise9();
            //Exercise10();
            //Exercise11();
            //Exercise12();
            //Exercise13();
            //Exercise14();
            //Exercise15();
            //Exercise16();
            //Exercise17();
            //Exercise18();
            //Exercise19();
            //Exercise20();
            //Exercise22();
            //Exercise23();
            //Exercise24();
            //Exercise25();
            //Exercise26();
            //Exercise27();
            //Exercise28();
            //Exercise29();
            //Exercise30();
            //Exercise31();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            //products.Where(p => p.UnitsInStock == 0)
            //use Where as the method
            // => lambda is equivalent to from and select
            var product = DataLoader.LoadProducts();
            var stock = from p in product
                        where p.UnitsInStock == 0
                        select p;

            PrintProductInformation(stock);
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            var product = DataLoader.LoadProducts();
            var sc = from p in product
                     where p.UnitsInStock >= 1
                     where p.UnitPrice >= 3
                     select p;

            PrintProductInformation(sc);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            var customer = DataLoader.LoadCustomers();
            var washington = from c in customer
                             where c.Region == "WA"
                             select c;

            PrintCustomerInformation(washington);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            var collection = from p in DataLoader.LoadProducts()
                          select new
                          {
                              name = p.ProductName
                          };
            foreach (var item in collection)
            {
                Console.WriteLine(item.name);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            var product = from p in DataLoader.LoadProducts()
                          select new
                          {
                              p.ProductID,
                              p.ProductName,
                              p.Category,
                              UnitPrice = p.UnitPrice * 1.25m,
                              p.UnitsInStock
                          };
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");
            foreach (var item in product)
            {
                Console.WriteLine(line, item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock);
            }


        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            var product = from p in DataLoader.LoadProducts()
                          select new
                          {
                              p.ProductName,
                              p.Category
                          };
            string line = "{0,-5} {1,-35} {2,-15}";
            Console.WriteLine(line, "", "Product Name", "Category");
            Console.WriteLine("==============================================================================");
            foreach (var item in product)
            {
                Console.WriteLine(line, "", item.ProductName.ToUpper(), item.Category.ToUpper());
            }

        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void Exercise7()
        {
            var product = from p in DataLoader.LoadProducts()
                          select new
                          {
                              p.ProductID,
                              p.ProductName,
                              p.Category,
                              p.UnitPrice,
                              p.UnitsInStock,
                              ReOrder = p.UnitsInStock < 3 ? true : false
                          };
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "ReOrder");
            Console.WriteLine("==============================================================================");
            foreach (var item in product)
            {
                Console.WriteLine(line, item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock, item.ReOrder);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            var product = from p in DataLoader.LoadProducts()
                          select new
                          {
                              p.ProductID,
                              p.ProductName,
                              p.Category,
                              p.UnitPrice,
                              p.UnitsInStock,
                              StockValue = p.UnitPrice * p.UnitsInStock
                          };
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,10:c}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "StockValue");
            Console.WriteLine("==============================================================================");
            foreach (var item in product)
            {
                Console.WriteLine(line, item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock, item.StockValue);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            var even = from na in DataLoader.NumbersA
                        where na % 2 == 0
                        select na;

            foreach (var na in even)
            {
                Console.WriteLine(na + " ");
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            var customers = DataLoader.LoadCustomers();
            var price = from p in customers
                        select new
                        {
                            customer = p,
                            //Orders O is taking the input object Sum and getting an output value Total
                            price = p.Orders.Sum(o => o.Total)
                        };
            var final = from f in price
                            //f.price is the condenced version of the body in select new, above
                        where f.price < 500.0M
                        select f.customer;
            PrintCustomerInformation(final);

        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            var odd = (from o in DataLoader.NumbersC
                       //remainder of 1 is an odd answer
                       where o % 2 == 1
                       select o).Take(3);

            foreach (var o in odd)
            {
                Console.WriteLine(o + " ");
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            var num = (DataLoader.NumbersB).Skip(3);
            foreach (var n in num)
            {
                Console.WriteLine(n + " ");
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            //var customers = DataLoader.LoadCustomers().Where(c => c.Region == "WA").Select(c => c); the exact same thing
            //Select or Group always has to be last!!
            var customers = DataLoader.LoadCustomers()
                .Where(c => c.Region == "WA")
                .Select(c => c);
            var location = from c in customers
                           select new
                           {
                               c.CompanyName,
                               MostRecent = c.Orders
                               .OrderBy(order => order.OrderDate)
                               .Last()
                           };

            foreach (var item in location)
            {
                Console.WriteLine(item.CompanyName + item.MostRecent.OrderDate + item.MostRecent.Total);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            var num = (DataLoader.NumbersC).TakeWhile(n => n <= 6);
            foreach (var n in num)
            {
                Console.WriteLine(n + " ");
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            var num = (DataLoader.NumbersC).SkipWhile(n => n % 3 != 0);
            foreach (var n in num)
            {
                Console.WriteLine(n + " ");
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            var product = DataLoader.LoadProducts();
            var alph = product.OrderBy(p => p.ProductName);

            PrintProductInformation(alph);
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            var product = DataLoader.LoadProducts();
            var decend = product.OrderByDescending(i => i.UnitsInStock);

            PrintProductInformation(decend);

        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            var product = DataLoader.LoadProducts();
            var order = product.OrderByDescending(o => o.UnitPrice).OrderBy(o => o.Category);

            PrintProductInformation(order);
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            var num = (DataLoader.NumbersB).Reverse();
            foreach (var n in num)
            {
                Console.WriteLine(n + " ");
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            var product = DataLoader.LoadProducts();
            var cat = from c in product
                      group c by c.Category;

            foreach (var p in cat)
            {
                Console.WriteLine(p.Key);
                foreach (var c in p)
                {
                    Console.WriteLine(c.ProductName);
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        //static void Exercise21()
        //{
        //    var q = from c in Customer
        //            select new
        //            {
        //                CustomerName = c.Name,
        //                Years = from o in c.Orders
        //                        group o by o.OrderDate.Date into gYear
        //                        select new
        //                        {
        //                            year = gYear.Key,
        //                            Months = from o in gYear
        //                                     group o by o.OrderDateYear.Month gMonth
        //                                     select new
        //                                     {
        //                                         Month = gMonth.Key,
        //                                         Orders = gMonth
        //                                     };
        //                        }
        //            }
        //}

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {
            var product = DataLoader.LoadProducts();
            var cat = from c in product
                      group c by c.Category;

            foreach (var p in cat)
            {
                Console.WriteLine(p.Key);
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            //bool check = DataLoader.LoadProducts().Any(x => x.ProductID == 789);
            //Console.WriteLine(check);
            bool tf = false;
            var question = DataLoader.LoadProducts().Where(t => t.ProductID == 789);
            if (question == null)
            {
                tf = true;
            }
            Console.WriteLine(tf);
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            var products = DataLoader.LoadProducts();
            var order = from o in products
                        where o.UnitsInStock == 0
                        group o by o.Category;
            foreach (var o in order)
            {
                Console.WriteLine(o.Key);
            }
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            var products = DataLoader.LoadProducts();
            var order = from o in products
                        where o.UnitsInStock != 0
                        group o by o.Category;
            foreach (var o in order)
            {
                Console.WriteLine(o.Key);
            }
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            var numbers = DataLoader.NumbersA;
            var counter = from n in numbers
                          where n % 2 == 1
                          select n;
            int odd = counter.Count();
            Console.WriteLine(odd);

        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {
            var customer = DataLoader.LoadCustomers();
            var print = from c in customer
                        select new
                        {
                            id = c.CustomerID,
                            Count = c.Orders.Count()
                        };
            foreach (var p in print)
            {
                Console.WriteLine($"{p.id}            {p.Count}");
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            var product = DataLoader.LoadProducts();
            var count = from p in product
                        group p by p.Category;

            foreach (var p in count)
            {
                Console.WriteLine($"{p.Key}          {p.Count()}");
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            var product = from p in DataLoader.LoadProducts()
                          group p by p.Category into stock
                          select new
                          {
                              stock.Key,
                              Count = stock.Sum(p => p.UnitsInStock)
                          };
            foreach (var p in product)
            {
                Console.WriteLine($"{p.Key}          {p.Count}");
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            var product = from p in DataLoader.LoadProducts()
                          group p by p.Category into stock
                          select new
                          {
                              stock.Key,
                              min = stock.Min(p => p.UnitPrice)
                          };
            foreach (var p in product)
            {
                Console.WriteLine($"{p.Key}          {p.min}");
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            var product = (from p in DataLoader.LoadProducts()
                          group p by p.Category into unit
                          orderby unit.Average(p => p.UnitPrice)
                           select new
                          {
                              unit.Key,
                              av = unit.Average(p => p.UnitPrice)
                          }).Take(3);

            foreach (var p in product)
            {
                Console.WriteLine($"{p.Key}          {p.av}");
            }
        }
    }
}
