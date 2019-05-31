using MasteryFlooring.Models;
using System;

namespace MasteryFlooring.Data.Mappers
{
    public class ProductMapper
    {
        public static Product StringToProduct(string row)
        {
            string[] fields = row.Split(new string[] { "::" }, StringSplitOptions.None);

            if (fields[0] == "ProductType")
                return null;
            Product result = new Product()
            {
                ProductType = fields[0],
                CostPerSquareFoot = decimal.Parse(fields[1]),
                LaborCostPerSquareFoot = decimal.Parse(fields[2])
            };
            return result;
        }

        public static string ProductToString(Product product)
        {
            return $"{product.ProductType}::{product.CostPerSquareFoot}::{product.LaborCostPerSquareFoot}";
        }
    }
}
