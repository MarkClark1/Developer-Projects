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
    public class TaxRepository
    {
        public static List<Tax> GetTaxes()
        {
            List<Tax> taxes = new List<Tax>();
            string[] rows = File.ReadAllLines(@"C:\Users\nthny\Documents\Bitbucket\anthony-dahl-individual-work\MasteryFlooring\MasteryFlooring.Data\Taxes.txt");
            foreach (string row in rows)
            {
                if (row != null)
                {
                    taxes.Add(TaxMapper.StringToTax(row));
                }
            }
            return taxes;
        }
    }
}
