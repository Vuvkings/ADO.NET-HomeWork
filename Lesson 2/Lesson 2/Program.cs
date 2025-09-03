using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lesson_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            //using (SqlConnection conn = new SqlConnection(constr))
            //{
            //    conn.Open();

                //    //1 [dbo].[stp_CustomerAll] ------------------------------------------------------------------------------
                //    string customerAll = "[dbo].[stp_CustomerAll]";
                //    SqlCommand cmd = new SqlCommand(customerAll, conn);

                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //    SqlDataReader dataReader = cmd.ExecuteReader();

                //    while (dataReader.Read())
                //    {
                //        //var f0 = dataReader["EmployeeID"];
                //        //var f1 = dataReader["LastName"];
                //        //var f2 = dataReader["Salary"];

                //        Console.WriteLine($"{dataReader["id"],4}{dataReader["LastName"],15}{dataReader["DateOfBirth"],20}");
                //    }
                //    Console.WriteLine("\n-----------------------------------------------------------------\n");

                //    dataReader.Close();

                //    //2 [dbo].[stp_CustomerAdd] ------------------------------------------------------------------------------
                //    string cust_add = "[dbo].[stp_CustomerAdd]";
                //    SqlCommand cmd2 = new SqlCommand(cust_add, conn);

                //    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                //    cmd2.Parameters.AddWithValue("@FirstName", "Ella");
                //    cmd2.Parameters.AddWithValue("@LastName", "Chornogor");
                //    cmd2.Parameters.AddWithValue("@DateOfBirth", DateTime.Now.ToShortDateString());

                //    SqlParameter cust_id = cmd2.Parameters.Add("@CustomerID", System.Data.SqlDbType.Int);
                //    cust_id.Direction = ParameterDirection.Output; //определяем параметр как выходной

                //    cmd2.ExecuteNonQuery();

                //    Console.WriteLine((int)cust_id.Value);
                //    Console.WriteLine("\n-----------------------------------------------------------------\n");
                //}


                ////ДЗ - хххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххххх

                string constr = ConfigurationManager.ConnectionStrings["Game_Shop_db"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                SqlDataReader reader;

                //[dbo].[Add_product] - добавление продукта
                string addProduct = "[dbo].[Add_product]";
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = addProduct,
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@title", "GTA VI");
                cmd.Parameters.AddWithValue("@description", "Экшн игра с открытым миром");
                cmd.Parameters.AddWithValue("@price", 4000.00);

                int res = cmd.ExecuteNonQuery();

                Console.WriteLine($"Обновлено строк: {res}");
                Console.WriteLine("\n-----------------------------------------------------------------\n");


                //[dbo].[Get_product_by_category] - Получение продукта по категории
                string getProductByCategory = "[dbo].[Get_product_by_category]";
                SqlCommand cmd2 = new SqlCommand(getProductByCategory, conn);

                int category_id = 2;

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@category_id", category_id);

                reader = cmd2.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}");
                }

                reader.Close();

                Console.WriteLine("\n-----------------------------------------------------------------\n");

                //[dbo].[Get_products_paginated] - Получение списка всех товаров (Порционно)
                string getProductsPaginated = "[dbo].[Get_products_paginated]";

                int pageNumber = 1;
                const int PageSize = 5;

                SqlCommand cmd3 = new SqlCommand(getProductsPaginated, conn);

                cmd3.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterPageNumber = new SqlParameter()
                {
                    ParameterName = "@page_number",
                    SqlDbType = SqlDbType.Int,
                    Value = pageNumber
                };

                cmd3.Parameters.Add(parameterPageNumber);
                cmd3.Parameters.AddWithValue("@page_size", PageSize);

                reader = cmd3.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Название - {reader[0]}");
                        Console.WriteLine($"Описание - {reader[1]}");
                        Console.WriteLine($"Общая стоимость - {reader[2]}");
                    }
                    reader.Close();

                    Console.WriteLine($"\tPage {pageNumber}\n");

                    pageNumber++;
                    cmd3.Parameters[0].Value = pageNumber;

                    reader = cmd3.ExecuteReader();
                }

                reader.Close();
            }
            }
    }
}
