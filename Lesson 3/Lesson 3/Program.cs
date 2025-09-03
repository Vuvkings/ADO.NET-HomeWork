using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Lesson_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
//using (SqlConnection conn = new SqlConnection(constr))
//{
//    conn.Open();

//    //2 [dbo].[stp_CustomerAdd] ------------------------------------------------------------------------------
//    //string cust_add = "[dbo].[stp_CustomerAdd]";
//    //SqlCommand cmd2 = new SqlCommand(cust_add, conn);
//    //cmd2.CommandType = System.Data.CommandType.StoredProcedure;
//    //cmd2.Parameters.AddWithValue("@FirstName", "Ella");
//    //cmd2.Parameters.AddWithValue("@LastName", "Chornogor");
//    //cmd2.Parameters.AddWithValue("@DateOfBirth", DateTime.Now.ToShortDateString());
//    //SqlParameter cust_id = cmd2.Parameters.Add("@CustomerID", System.Data.SqlDbType.Int);
//    //cust_id.Direction = ParameterDirection.Output; //определяем параметр как выходной
//    //cmd2.ExecuteNonQuery();
//    //Console.WriteLine((int)cust_id.Value);

//    //with output dbo.stp_CustomerAdd ------------------------------------------------------------------------------
//    //string custAdd = "dbo.stp_CustomerAdd";
//    //SqlCommand cmd = new SqlCommand(custAdd, conn);
//    //cmd.CommandType = CommandType.StoredProcedure;

//    //SqlCommandBuilder.DeriveParameters(cmd);
//    //cmd.Parameters[4].Value = DBNull.Value;
//    //cmd.Parameters[1].Value = "NewFirstName";
//    //cmd.Parameters[2].Value = "NewLastName";
//    //cmd.Parameters[3].Value = DateTime.Now.AddYears(-1).ToShortDateString();

//    //cmd.ExecuteNonQuery();
//    //int new_id = (int)cmd.Parameters[4].Value;

//    //Console.WriteLine(new_id);

//    //with return dbo.stp_CustomerAdd_2 ------------------------------------------------------------------------------
//    string custAdd = "dbo.stp_CustomerAdd_2";
//    SqlCommand cmd = new SqlCommand(custAdd, conn);
//    cmd.CommandType = CommandType.StoredProcedure;

//    SqlCommandBuilder.DeriveParameters(cmd);
//    cmd.Parameters[0].Value = DBNull.Value;
//    cmd.Parameters[1].Value = "NewFirstName2";
//    cmd.Parameters[2].Value = "NewLastName2";
//    cmd.Parameters[3].Value = DateTime.Now.AddYears(-1).ToShortDateString();

//    cmd.ExecuteNonQuery();
//    int new_id = (int)cmd.Parameters[0].Value;

//    Console.WriteLine(new_id);
//}


// ДЗ xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

string constr = ConfigurationManager.ConnectionStrings["Game_Shop__db"].ConnectionString;
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

    SqlCommandBuilder.DeriveParameters(cmd);
    cmd.Parameters[1].Value = "Беговая дорожка ProForm2";
    cmd.Parameters[2].Value = "Электрическая беговая дорожка с 12 программами тренировок, максимальная скорость 16 км/ч";
    cmd.Parameters[3].Value = 45990.00;

    int res = cmd.ExecuteNonQuery();

    Console.WriteLine($"Обновлено строк: {res}");
    Console.WriteLine("\n-----------------------------------------------------------------\n");


    //[dbo].[Get_product_by_category] - Получение продукта по категории
    string getProductByCategory = "[dbo].[Get_product_by_category]";
    SqlCommand cmd2 = new SqlCommand(getProductByCategory, conn);
    cmd2.CommandType = CommandType.StoredProcedure;

    int category_id = 1;

    SqlCommandBuilder.DeriveParameters(cmd2);
    cmd2.Parameters[1].Value = category_id;

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

    SqlCommandBuilder.DeriveParameters(cmd3);
    cmd3.Parameters[1].Value = pageNumber;
    cmd3.Parameters[2].Value = PageSize;

    reader = cmd3.ExecuteReader();

    while (reader.HasRows)
    {
        while (reader.Read())
        {
            Console.WriteLine($"Название - {reader[0]}");
            Console.WriteLine($"Описание - {reader[1]}");
            Console.WriteLine($"Общее количество - {reader[2]}");
        }
        reader.Close();

        Console.WriteLine($"\tPage {pageNumber}\n");

        pageNumber++;
        cmd3.Parameters[1].Value = pageNumber;

        reader = cmd3.ExecuteReader();
    }

    reader.Close();
            }
        }
    }
}

