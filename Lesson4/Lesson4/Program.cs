using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Lesson4.Models;


namespace Lesson4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CustomerModel cust1 = DL.Customer.ByID(1);
            //CustomerModel cust2 = DL.Customer.ByID(2);

            //Console.WriteLine(cust1);
            //Console.WriteLine(cust2);

            //int id = DL.Customer.Insert(new CustomerModel(0, "FN", "LN", new DateTime(2025, 3, 15)));
            //Console.WriteLine(id);

            //List<CustomerModel> allCustomers = DL.Customer.All();
            //foreach (var customer in allCustomers)
            //{
            //    Console.WriteLine(customer);
            //}


            //*********************ДЗ:  *****************************

            
            List<CustomerModel> allCustomers = DL.Customer.All();
            foreach (var customer in allCustomers)
            {
                Console.WriteLine(customer);
            }

            int res = DL.Customer.Delete(1005);

            allCustomers = DL.Customer.All();
            foreach (var customer in allCustomers)
            {
                Console.WriteLine(customer);
            }

            int id = DL.Customer.Insert(new CustomerModel(0, "FN", "LN", new DateTime(2000, 1, 1)));

            // Update()
            DL.Customer.Update(1006, null, "LN2", DateTime.Now);
            Console.WriteLine(DL.Customer.ByID(1006));

            DL.Customer.Update(1006, "FirstName2", "LastName2");
            Console.WriteLine(DL.Customer.ByID(1006));
        }
    }
}
