using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CIITSIS
{
    public partial class StudentList : Form
    {
        public StudentList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection con = new Connection();
            con.openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Users";
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            con.closeConection();
        }
    }
}
