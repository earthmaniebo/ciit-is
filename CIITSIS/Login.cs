using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web.Security;

namespace CIITSIS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string HashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            Connection con = new Connection();
            con.openConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Login where Username = @Username and " + "Password = @Password";
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = txtUserName.Text;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = HashedPassword;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MainForm m1 = new MainForm();
                dr.Close();
                con.closeConection();
                this.Hide();
                m1.ShowDialog();
            }

            else
            {
                MessageBox.Show("Username or Password does not exist!");
                dr.Close();
                con.closeConection();
            }

        }
    }
}
