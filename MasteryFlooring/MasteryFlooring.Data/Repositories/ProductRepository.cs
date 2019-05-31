using MasteryFlooring.Data.Mappers;
using MasteryFlooring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryFlooring.Data
{
    public class ProductRepository
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string[] rows = File.ReadAllLines(@"C:\Users\nthny\Documents\Bitbucket\anthony-dahl-individual-work\MasteryFlooring\MasteryFlooring.Data\Products.txt");
            foreach (string row in rows)
            {
                if (row != null)
                {
                    products.Add(ProductMapper.StringToProduct(row));
                }                
            }
            return products;
        }
    }
}
