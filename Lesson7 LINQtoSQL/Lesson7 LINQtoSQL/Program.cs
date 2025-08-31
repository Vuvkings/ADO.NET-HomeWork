using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Configuration;
using static System.Console;
namespace Lesson7_LINQtoSQL
{
    internal class Program
    {
        [Table (Name = "Customers")]
        internal class Customer
        {
            [Column (IsPrimaryKey = true, IsDbGenerated = true )]
            public int  Id { get; set; }
            public string FirstName { get; set; }
            
            public string LastName { get; set; }
           // [Column (CanBeNull = true)]
            public DateTime DateofBirth { get; set; }
            public override string ToString()
            {
                return $" {Id, 5} {FirstName,20} {LastName, 20} {Convert.ToDateTime(DateofBirth).ToShortDateString(),20}";
            }
          
        }
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            using (DataContext dk = new DataContext(constr))
            {
                Table<Customer> customers = dk.GetTable<Customer>();
                foreach (Customer item in customers) 
                {
                    WriteLine(item);
                }
                WriteLine("***************************");

                List<Customer> res = customers.Take(2).ToList();
                foreach (var item in res)
                {
                   WriteLine(item);
                }
                WriteLine("******* customer.Year>2015 *********");
                //var query = from e in customers
                //            where e.Id == 3
                //            select e;
                //foreach (Customer customer in query)
                //{
                //    Console.WriteLine(customer);
                //}
                WriteLine("******** customer.Year>2015 *******");
                //var query = from e in customers
                //            where e.DateofBirth.Year > 2015
                //            select e;
                //foreach (Customer customer in query)
                //{
                //    Console.WriteLine(customer);
                //}
                WriteLine("********customer.FirstName ->StartsWith(\"I\")********");

                //var query = from e in customers
                //            where e.FirstName.StartsWith("I")
                //            select e;
                //foreach (Customer item in query)
                //{
                //    Console.WriteLine(item);
                //}
                WriteLine("***************************");

                //Customer cust_new = new Customer
                //{
                //    DateofBirth = new DateTime(1999, 11, 1),
                //    LastName = "LN_customer",
                //    FirstName = "FN_new_customer"
                //};
                //customers.InsertOnSubmit(cust_new);
                //dk.SubmitChanges();
                //foreach (Customer item in customers)
                //{
                //    Console.WriteLine(item);
                //}

                // Customer c_edit = customers.Where(c => c.Id == 2).First();
                //c_edit.LastName += "_redacted";
                //Console.WriteLine(c_edit);
                //dc.SubmitChanges();
                //Console.WriteLine("+++++++++++++++++++++++++++++++++++");
                //foreach (Customer item in customers)
                //{
                //    Console.WriteLine(item);
                //}
            }
        }
    }
}
