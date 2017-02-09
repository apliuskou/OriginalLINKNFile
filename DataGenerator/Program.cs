using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    class Program
    {
        public static DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        static void Main(string[] args)
        {
            string homeFolder = @"c:\GitHub";
            Random random = new Random();
            string nextRecord = "";
            int maxCustomers = Enum.GetNames(typeof(Customers)).Length;
            try
            {
                using (StreamWriter target = File.CreateText(homeFolder + @"\sample.csv"))
                {

                    for (int i = 0; i < 10000; i++)
                    {
                        nextRecord = nextRecord + i + ", " + (Customers)random.Next(0, maxCustomers) + ", ";
                        nextRecord = nextRecord + random.Next(56, 10000) + ", ";
                        nextRecord = nextRecord + RandomDay();
                        Console.WriteLine(" Line {0}: {1}", i, nextRecord);
                        target.WriteLine(nextRecord);

                        nextRecord = "";
                    }
                }
            }
            catch (IOException)
            {

                Console.WriteLine("Error! File already exist or path not found!");
                throw;
            }

            Console.WriteLine("Saved successfully! Sample file built!");
            Console.ReadKey();
          
            }
        }
    }

