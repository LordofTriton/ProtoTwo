using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProtoTwo.Models
{
    public class UserDB
    {
        private SqlConnection conn; //Create a connection object
        //Initialize the connection
        private void connection()
        {
            string connstring = ConfigurationManager.ConnectionStrings["VMSDbConn"].ToString(); //The connection string is provided in the 'web.config' file
            conn = new SqlConnection(connstring);
        }

        //Verifies that user provided details exist in the Db
        public bool Verify(string Name, string Pass)
        {
            connection(); //Init connection

            List<Main> UserList = new List<Main>(); //Blank List

            SqlCommand cmd = new SqlCommand("GetUserList", conn); //Create an SQL Query string using the provided Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd); //Used to convert received data into different forms
            DataTable dt = new DataTable(); //DataTable? Need to look this up.

            conn.Open();
            sd.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                //Parse data into prepared list
                UserList.Add(
                    new Main
                    {
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Pass"])
                    });
            }

            //FIlter the UserList with the user provided details
            var check = UserList.Where(x => x.UserName == Name)
                .Where(x => x.Password == Pass).FirstOrDefault();

            return (check != null);
        }
    }
}