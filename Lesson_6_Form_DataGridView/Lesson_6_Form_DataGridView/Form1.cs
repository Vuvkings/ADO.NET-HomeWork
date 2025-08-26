using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson_6_Form_DataGridView
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string str = "";
        string filename = "";
        SqlCommandBuilder cmd = null;
        public Form1()
        {
            InitializeComponent();
            str = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            conn = new SqlConnection(str);
           
            richTextBox1.Text = "select * from";
        }

        private void Fill_Button_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(str);
                ds = new DataSet();
                string sqlq = richTextBox1.Text;
                da = new SqlDataAdapter(sqlq, conn);
                dataGridView1.DataSource = null;
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, "Table_1");
                dataGridView1.DataSource = ds.Tables["Table_1"];
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                conn.Close();
            }
            
        }

        private void Update_Button_Click(object sender, EventArgs e)
        {
            da.Update(ds, "Table_1");
        }
    }
}
