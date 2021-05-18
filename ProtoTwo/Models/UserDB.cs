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
        private SqlConnection conn;
        private void connection()
        {
            string connstring = ConfigurationManager.ConnectionStrings["VMSDbConn"].ToString();
            conn = new SqlConnection(connstring);
        }

        public bool Verify(string Name, string Pass)
        {
            connection();
            List<Main> UserList = new List<Main>();

            SqlCommand cmd = new SqlCommand("GetUserList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sd.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                UserList.Add(
                    new Main
                    {
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Pass"])
                    });
            }

            var check = UserList.Where(x => x.UserName == Name)
                .Where(x => x.Password == Pass).FirstOrDefault();

            return (check != null);
        }
    }
}