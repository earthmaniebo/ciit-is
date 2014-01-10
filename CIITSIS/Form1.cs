﻿using System;
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
    public partial class Form1 : Form
    {
        SqlConnection cn;
        private static int userID = 0;
        public int UsersID;
        public bool Update;
        public int CourseID;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string HashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            if (txtAddress.Text == "" || txtFN.Text == "" || txtLN.Text == "" || txtUserName.Text == ""
                    || txtPassword.Text == "")
            {
                MessageBox.Show("Please fill in all the required fields!");
            }

            else if (txtPassword.Text != txtcPassword.Text)
            {
                MessageBox.Show("Password does not match!");
            }

            else if (DateTime.Parse(txtBD.Text).Year > (DateTime.Now.Year - 10))
            {
                MessageBox.Show("10 yrs old and below not allowed!");
            }
            if (Update == true)
            {
                Connection cn = new Connection();
                cn.openConnection();
                SqlTransaction trn = cn.cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cn;
                cmd.Transaction = trn;
                cmd.CommandText = "Update Users set LastName = @LastName, FirstName = @FirstName, MiddleName = @MiddleName, BirthDate = @BirthDate, CourseID = @CourseID, Address = @Address where UserID = @UserID " + "Update Login set UserName = @UserName, Password = @Password, UserTypeID = @UserTypeID where UserID = @UserID";

                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLN.Text;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFN.Text;
                cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = txtMN.Text;
                cmd.Parameters.Add("@BirthDate", SqlDbType.NVarChar).Value = txtBD.Text;
                cmd.Parameters.Add("@CourseID", SqlDbType.NVarChar).Value = txtCourse.SelectedValue.ToString();
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtAddress.Text;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = txtUserName.Text;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = HashedPassword;
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UsersID.ToString();
                cmd.Parameters.Add("@UserTypeID", SqlDbType.NVarChar).Value = txtUserType.SelectedValue.ToString();
                cmd.ExecuteNonQuery();
                trn.Commit();
                cn.closeConection();
                
                MessageBox.Show("Successfully Updated!");
            }

            else
            {
                if (txtAddress.Text == "" || txtFN.Text == "" || txtLN.Text == "" || txtUserName.Text == ""
                    || txtPassword.Text == "")
                {
                    MessageBox.Show("Please fill in all the required fields!");
                }

                else if (txtPassword.Text != txtcPassword.Text)
                {
                    MessageBox.Show("Password does not match!");
                }

                else if (DateTime.Parse(txtBD.Text).Year > (DateTime.Now.Year - 10))
                {
                    MessageBox.Show("10 yrs old and below not allowed!");
                }

                else
                {
                    //                cn = new SqlConnection();
                    //                cn.ConnectionString = @"Server = Earth-PC\SQLEXPRESS;Database = ManieboE;
                    //                                        Integrated Security = false; User ID = sa;Password = 1234";
                    //                cn.Open();
                    Connection con = new Connection();
                    con.openConnection();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con.cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT into Users values(@LastName,@FirstName,@MiddleName,@Birthdate,@CourseID,@Address)";
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLN.Text;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFN.Text;
                    cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = txtMN.Text;
                    cmd.Parameters.Add("@BirthDate", SqlDbType.NVarChar).Value = txtBD.Text;
                    cmd.Parameters.Add("@CourseID", SqlDbType.NVarChar).Value = txtCourse.SelectedValue.ToString();
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtAddress.Text;

                    cmd.ExecuteNonQuery();

                    SqlCommand cmdSelect = new SqlCommand();
                    cmdSelect.Connection = con.cn;
                    cmdSelect.CommandType = CommandType.Text;
                    cmdSelect.CommandText = "Select top 1 UserID from Users order by UserID Desc";
                    SqlDataReader dr = cmdSelect.ExecuteReader();

                    while (dr.Read())
                    {
                        userID = int.Parse(dr["UserID"].ToString());
                    }
                    dr.Close();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = con.cn;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "Insert into Login values(@UserName,@Password,@UserID,@UserTypeID,@DateCreated,@isEnabled)";
                    cmd1.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = txtUserName.Text;
                    cmd1.Parameters.Add("@Password", SqlDbType.NVarChar).Value = HashedPassword;
                    cmd1.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = userID.ToString();
                    cmd1.Parameters.Add("@UserTypeID", SqlDbType.NVarChar).Value = txtUserType.SelectedValue.ToString();
                    cmd1.Parameters.Add("@DateCreated", SqlDbType.NVarChar).Value = DateTime.Now.Date;
                    cmd1.Parameters.Add("@isEnabled", SqlDbType.NVarChar).Value = "y";
                    cmd1.ExecuteNonQuery();
                    //cn.Close();
                    con.closeConection();

                    MessageBox.Show("Successfully Added!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Update == true)
            {
                this.userTypeTableAdapter.Fill(this.manieboEDataSet1.UserType);
                this.courseTableAdapter1.Fill(this.manieboEDataSet2.Course);
                txtCourse.SelectedValue = CourseID;
            }
            else
            {
                // TODO: This line of code loads data into the 'manieboEDataSet2.Course' table. You can move, or remove it, as needed.
                this.courseTableAdapter1.Fill(this.manieboEDataSet2.Course);
                // TODO: This line of code loads data into the 'manieboEDataSet1.UserType' table. You can move, or remove it, as needed.
                this.userTypeTableAdapter.Fill(this.manieboEDataSet1.UserType);
                // TODO: This line of code loads data into the 'manieboEDataSet.Course' table. You can move, or remove it, as needed.
                this.courseTableAdapter.Fill(this.manieboEDataSet.Course);
            }
        }

        private void bntCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}