using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Lesson4.Models;
namespace Lesson4
{
    internal class DL
    {
        public static string ConnectionString { get; private set; } = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
        static SqlConnection conn;

        public static class Customer
        {
            public static CustomerModel ByID(int customerID)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    SqlCommand getCustomer = new SqlCommand("stp_CustomerByID", conn);
                    getCustomer.Parameters.AddWithValue("@customerID", customerID);
                    getCustomer.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = getCustomer.ExecuteReader();

                    CustomerModel customer = null;

                    while (reader.Read())
                    {
                        int ID = (int)reader[0];
                        string FirstName = reader[1].ToString();
                        string LastName = reader[2].ToString();
                        DateTime birthDate = DateTime.Parse(reader[3].ToString());
                        customer = new CustomerModel(ID, FirstName, LastName, birthDate);
                    }

                    reader.Close();

                    return customer;
                }
            }

            public static int Insert(CustomerModel customer)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string custAdd = "dbo.stp_CustomerAdd";
                    SqlCommand cmd = new SqlCommand(custAdd, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlCommandBuilder.DeriveParameters(cmd);
                    cmd.Parameters[4].Value = DBNull.Value;
                    cmd.Parameters[1].Value = customer.FirstName;
                    cmd.Parameters[2].Value = customer.LastName;
                    cmd.Parameters[3].Value = customer.DateOfBirth;

                    cmd.ExecuteNonQuery();
                    int new_id = (int)cmd.Parameters[4].Value;

                    return new_id;
                }
            }

            public static List<CustomerModel> All()
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string customerAll = "[dbo].[stp_CustomerAll]";
                    SqlCommand cmd = new SqlCommand(customerAll, conn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    List<CustomerModel> list = new List<CustomerModel>();
                    while (dataReader.Read())
                    {
                        list.Add(new CustomerModel
                        (
                            (int)dataReader["id"],
                            dataReader["FirstName"].ToString(),
                            dataReader["LastName"].ToString(),
                            (DateTime)dataReader["DateOfBirth"]
                        ));
                    }

                    dataReader.Close();

                    return list;
                }
            }

            public static int Delete(int customerID)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string custDelete = "dbo.stp_CustomerDelete";
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = custDelete,
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@customerID", customerID);
                    int res = cmd.ExecuteNonQuery();

                    Console.WriteLine($"Обновлено строк: {res}");

                    return res;
                }
            }

            public static int Update(int id, string firstName = null, string lastName = null, DateTime? dateOfBirth = null, byte[] pictureBytes = null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string custUodate = "dbo.stp_CustomerUpdate";
                    SqlCommand cmd = new SqlCommand(custUodate, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customerID", id);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                    cmd.Parameters.AddWithValue("@Picture", pictureBytes);

                    int res = cmd.ExecuteNonQuery();

                    Console.WriteLine($"Обновлено строк: {res}");

                    return res;
                }
            }
        }
    }
}
