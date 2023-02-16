using System;
using System.Collections.Generic;
using System.Text;
using LibraryBusiness;
using System.Configuration;
using System.Xml;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

namespace LibraryApplication
{
    public class Program{
        static void Main(string[] args){

            string country = "";
            DateTime startDate;
            DateTime endDate;

            if (args == null || args.Length != 3) {

                Console.WriteLine("Enter country code:");
                country = Console.ReadLine();

                Console.WriteLine("Enter start date (yyyy-MM-dd):");
                string startDateString = Console.ReadLine();
                startDate = DateTime.ParseExact(startDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter end date (yyyy-MM-dd):");
                string endDateString = Console.ReadLine();
                endDate = DateTime.ParseExact(endDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                country = args[0];
                startDate = Convert.ToDateTime(args[1]);
                endDate = Convert.ToDateTime(args[2]);
            }

            String fee = "";
            try{
                fee = new PenaltyFeeCalculator().Calculate(startDate,endDate, country);
            }
            catch (Exception e) {
                PrintErrorMessage(e);
            }
            PrintResultMessage(fee);
           
        }
        private static void PrintAnyKeyMessage(){
            Console.WriteLine("Press any key to continue");
        }
        private static void PrintResultMessage(string fee){
            Console.WriteLine("Penalty Fee is {0}", fee);
            PrintAnyKeyMessage();
            Console.ReadKey();
        }
        private static void PrintErrorMessage(Exception e) {
            Console.WriteLine("Exception : " + e.Message);
            Console.WriteLine("Stacktrace : ");
            Console.WriteLine(e.StackTrace);
            PrintAnyKeyMessage();
            Console.ReadKey();
        }
    }

}
