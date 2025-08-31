using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
namespace Leson1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connect = "Server=DESKTOP-GCERK5B;Database=MyDataBase;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(connect);

            conn.Open();
            {

                string sqlcommand = "select * from [MyDataBase].[dbo].[Employee] ";
                SqlCommand cmd = new SqlCommand(sqlcommand, conn);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var f0 = dr[0];
                    var f1 = dr[2];
                    var f2 = dr[5];
                    Console.WriteLine($" {f0,4}{f1,15}{f2,20}");

                }
                dr.Close();

                Console.WriteLine("Homework:");
           


            SqlDataReader dataReader;

            string sqlCommand1 = "select LastName, FirstName, Salary\r\nfrom MyDataBase.dbo.Employee\r\nwhere Salary between\r\n    (select (min(salary) + 1) from MyDataBase.dbo.Employee)\r\n    and\r\n    (select (max(salary) - 1) from MyDataBase.dbo.Employee)";
            SqlCommand cmd1 = new SqlCommand(sqlCommand1, conn);
            dataReader = cmd1.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader["LastName"],10}{dataReader["FirstName"],10}{dataReader["Salary"],10}");
            }

            dataReader.Close();

            Console.WriteLine("\n-----------------------------------------------------------------\n");

            string sqlCommand2 = "select FirstName, LastName\r\nfrom MyDataBase.dbo.Employee\r\nwhere LastName like '%ov'";
            SqlCommand cmd2 = new SqlCommand(sqlCommand2, conn);
            dataReader = cmd2.ExecuteReader();

            while (dataReader.Read())
            {
                var value1 = dataReader.GetValue(0);
                var value2 = dataReader.GetValue(1);

                Console.WriteLine($"{value1,10}{value2,10}");
            }

            dataReader.Close();

                string sqlCommand3 = "select\r\n    FirstName,\r\n    LastName,\r\n    (select PositionName from MyDataBase.dbo.Position\r\n    where MyDataBase.dbo.Position.PositionID = MyDataBase.dbo.Employee.PositionID) as PositionName\r\nfrom MyDataBase.dbo.Employee";
                SqlCommand cmd3 = new SqlCommand(sqlCommand3, conn);
                dataReader = cmd3.ExecuteReader();

                while (dataReader.Read())
                {
                    var value1 = dataReader.GetValue(0).ToString();
                    var value2 = dataReader.GetValue(1).ToString();
                    var value3 = dataReader.GetValue(2).ToString();

                    Console.WriteLine($"{value1,10}{value2,10}{value3,15}");
                }

                dataReader.Close();


            }
            conn.Close();
           
        }
    }
}
