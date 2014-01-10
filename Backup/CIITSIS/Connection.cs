using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CIITSIS
{
    class Connection
    {
        public SqlConnection cn;
        public void openConnection()
        {
            cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.AppSettings["myConnectionString"].ToString();
            cn.Open();
        }

        public void closeConection()
        {
            if (cn != null)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }
    }
}
