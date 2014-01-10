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
            cmd.CommandText = "Select Users.LastName, Users.FirstName, Users.MiddleName, Users.BirthDate, Course.CourseCode, Course.CourseID, Users.Address, Users.UserID from Users, Course where Course.CourseID = Users.CourseID";
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            con.closeConection();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            fr1.Update = true;
            fr1.txtLN.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            fr1.txtFN.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            fr1.txtMN.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            fr1.txtBD.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            fr1.CourseID = int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            fr1.txtAddress.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            fr1.UsersID = int.Parse(dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            fr1.ShowDialog();
        }
    }
}
