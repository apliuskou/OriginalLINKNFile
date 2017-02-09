using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<int, Transaction> transactions = new Dictionary<int, Transaction>();

            try
            {
                using (StreamReader sr = new StreamReader(@"c:\GitHub\Sample.csv"))
                {

                    while (!sr.EndOfStream)
                    {
                        string nextLine = sr.ReadLine();
                        string[] allFields = nextLine.Split(',');

                        Transaction nextTransaction = new Transaction();

                        try
                        {
                            nextTransaction.Id = Convert.ToInt32(allFields[0]);
                            Customers tempCustomerCode;
                            if (!Enum.TryParse(allFields[1], out tempCustomerCode))
                            {
                                throw new Exception("Cannot convert Customer: ");
                            }
                            nextTransaction.Customer = tempCustomerCode;
                            nextTransaction.Money = Convert.ToDecimal(allFields[2]);
                            nextTransaction.Date = Convert.ToDateTime(allFields[3]);
                            transactions[nextTransaction.Id] = nextTransaction;
                            Console.WriteLine("Successfully added transaction, totaling #" + transactions.Count);                                                

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("Cannot convert record: " + nextLine + ":" + ex.Message);
                            break;
                        }


                    }
                }
            }

            catch (IOException io)
            {
                Console.WriteLine("File missing or corrupt! " + io.Message );             
            }

            var queryResult = from transaction in transactions
                              group transaction by transaction.Value.Customer
                              into customerGroups
                              select customerGroups;

            foreach (var customerGroup in queryResult)
            {
                Decimal runningTotal = 0;
                string customerName = "";
                foreach (var item in customerGroup)
                {
                    runningTotal += item.Value.Money;
                    customerName = item.Value.Customer.ToString();
                }
                Console.WriteLine(customerName + " has placed orders totaling $" + runningTotal);

            }


            Console.ReadKey();

        
        }
    }
}
