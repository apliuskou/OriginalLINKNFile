using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalyzer
{
    public class Transaction 
    {
        public int Id { get; set; }
        public Customers Customer { get; set; }
        public decimal Money { get; set; }
        public DateTime Date { get; set; }

    }
}
