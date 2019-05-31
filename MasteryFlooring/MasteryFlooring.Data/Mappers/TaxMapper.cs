using MasteryFlooring.Models;
using System;

namespace MasteryFlooring.Data.Mappers
{
    public class TaxMapper
    {
        public static Tax StringToTax(string row)
        {
            string[] fields = row.Split(new string[] { "::" }, StringSplitOptions.None);

            if (fields[0] == "StateAbbreviation")
                return null; 
            Tax result = new Tax()
            {
                StateAbbreviation = fields[0],
                StateName = fields[1],
                TaxRate = decimal.Parse(fields[2])
            };
            return result;
        }

        public static string TaxToString(Tax tax)
        {
            return $"{tax.StateAbbreviation}::{tax.StateName}::{tax.TaxRate}";
        }
    }
}
