using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProtoTwo.Models
{
    public class VisitorDB
    {
        //See GuestDB.cs for explanatory comments

        private string tagGen()
        {
            Random rd = new Random();
            return Convert.ToString(rd.Next(10000, 99999));
        }

        private SqlConnection conn;
        private void connection()
        {
            string connstring = ConfigurationManager.ConnectionStrings["VMSDbConn"].ToString();
            conn = new SqlConnection(connstring);
        }

        public void AddNewVisitor(Main model)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewVisitor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
            cmd.Parameters.AddWithValue("@LastName", model.LastName);
            cmd.Parameters.AddWithValue("@Residence", model.Residence);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Check_In", model.Check_In);
            cmd.Parameters.AddWithValue("@Check_Out", "");
            cmd.Parameters.AddWithValue("@Purpose", model.Purpose);
            cmd.Parameters.AddWithValue("@Meeting", model.Meeting);
            cmd.Parameters.AddWithValue("@Tag", tagGen());
            cmd.Parameters.AddWithValue("@RegDate", DateTime.Now.ToString("d"));
            cmd.Parameters.AddWithValue("@Email", model.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public List<Main> GetVisitorList()
        {
            connection();
            List<Main> VisitorList = new List<Main>();

            SqlCommand cmd = new SqlCommand("GetVisitorList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sd.Fill(dt);
            conn.Close();

            foreach(DataRow dr in dt.Rows)
            {
                VisitorList.Add(
                    new Main
                    {
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Residence = Convert.ToString(dr["Residence"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Check_In = Convert.ToString(dr["Check_In"]),
                        Check_Out = Convert.ToString(dr["Check_Out"]),
                        Purpose = Convert.ToString(dr["Purpose"]),
                        Meeting = Convert.ToString(dr["Meeting"]),
                        Tag = Convert.ToString(dr["Tag"]),
                        RegDate = Convert.ToString(dr["RegDate"]),
                        Email = Convert.ToString(dr["Email"])
                    });
            }
            return VisitorList;
        }
        public void UpdateVisitorList(Main model)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateVisitorList", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
            cmd.Parameters.AddWithValue("@LastName", model.LastName);
            cmd.Parameters.AddWithValue("@Residence", model.Residence);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Check_In", model.Check_In);
            cmd.Parameters.AddWithValue("@Check_Out", "");
            cmd.Parameters.AddWithValue("@Purpose", model.Purpose);
            cmd.Parameters.AddWithValue("@Meeting", model.Meeting);
            cmd.Parameters.AddWithValue("@Tag", 06785);
            cmd.Parameters.AddWithValue("@RegDate", DateTime.Now.ToString("F"));
            cmd.Parameters.AddWithValue("@Email", model.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void CheckOutVisitor(string Tag)
        {
            connection();
            SqlCommand cmd = new SqlCommand("CheckOutVisitor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tag", Tag);
            cmd.Parameters.AddWithValue("@timeout", DateTime.Now.ToString("t"));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteVisitor(string Tag)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteVisitor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tag", Tag);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}