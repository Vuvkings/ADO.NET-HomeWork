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
            string constr = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;

            using(SqlConnection conn = new SqlConnection(constr)) { 
                conn.Open();

                string cust_dbo = "dbo.stp_CustomerAdd_2";
                SqlCommand cmd = new SqlCommand(cust_dbo, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters[0].Value=DBNull.Value;
                cmd.Parameters[1].Value = "NeFn_10";
                cmd.Parameters[2].Value = "NeLn_10";
                cmd.Parameters[3].Value = DateTime.Now;


                cmd.ExecuteNonQuery();
                int new_id = (int)cmd.Parameters[0].Value;
                Console.WriteLine(new_id);
            }
        }
    }
}
