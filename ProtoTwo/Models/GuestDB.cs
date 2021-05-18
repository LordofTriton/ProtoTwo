using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProtoTwo.Models
{
    public class GuestDB
    {
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

        public List<Main> GetCGuests()
        {
            connection();
            List<Main> CGuestList = new List<Main>();
            SqlCommand cmd = new SqlCommand("GetCGuestList", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sd.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                CGuestList.Add(
                    new Main
                    {
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Residence = Convert.ToString(dr["Residence"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Time = Convert.ToString(dr["Time"]),
                        Purpose = Convert.ToString(dr["Purpose"]),
                        Meeting = Convert.ToString(dr["Meeting"]),
                        Tag = Convert.ToString(dr["Tag"]),
                        RegDate = Convert.ToString(dr["RegDate"]),
                        Email = Convert.ToString(dr["Email"])
                    });
            }
            return CGuestList;
        }
        public List<Main> GetPGuests()
        {
            connection();
            List<Main> PGuestList = new List<Main>();
            SqlCommand cmd = new SqlCommand("GetPGuestList", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sd.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                PGuestList.Add(
                    new Main
                    {
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Residence = Convert.ToString(dr["Residence"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Time = Convert.ToString(dr["Time"]),
                        Purpose = Convert.ToString(dr["Purpose"]),
                        Meeting = Convert.ToString(dr["Meeting"]),
                        Tag = Convert.ToString(dr["Tag"]),
                        RegDate = Convert.ToString(dr["RegDate"]),
                        Email = Convert.ToString(dr["Email"])
                    });
            }
            return PGuestList;
        }
        public void RecordGuest(string Tag)
        {
            connection();
            SqlCommand cmd = new SqlCommand("RecordGuest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var lst = Main.CGuestList
                .Where(s => s.Tag == Tag).FirstOrDefault();

            cmd.Parameters.AddWithValue("@FirstName", lst.FirstName);
            cmd.Parameters.AddWithValue("@LastName", lst.LastName);
            cmd.Parameters.AddWithValue("@Residence", lst.Residence);
            cmd.Parameters.AddWithValue("@Phone", lst.Phone);
            cmd.Parameters.AddWithValue("@Check_In", @DateTime.Now.ToString("t"));
            cmd.Parameters.AddWithValue("@Check_Out", "");
            cmd.Parameters.AddWithValue("@Purpose", lst.Purpose);
            cmd.Parameters.AddWithValue("@Meeting", lst.Meeting);
            cmd.Parameters.AddWithValue("@Tag", lst.Tag);
            cmd.Parameters.AddWithValue("@RegDate", lst.RegDate);
            cmd.Parameters.AddWithValue("@Email", lst.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void AddNewGuest(Main model)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewGuest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
            cmd.Parameters.AddWithValue("@LastName", model.LastName);
            cmd.Parameters.AddWithValue("@Residence", model.Residence);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Time", model.Time);
            cmd.Parameters.AddWithValue("@Purpose", model.Purpose);
            cmd.Parameters.AddWithValue("@Meeting", model.Meeting);
            cmd.Parameters.AddWithValue("@Tag", tagGen());
            cmd.Parameters.AddWithValue("@RegDate", model.RegDate);
            cmd.Parameters.AddWithValue("@Email", model.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void ConfirmGuest(string Tag)
        {
            connection();
            SqlCommand cmd = new SqlCommand("ConfirmGuest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var lst = Main.PGuestList
                .Where(s => s.Tag == Tag).FirstOrDefault();

            cmd.Parameters.AddWithValue("@FirstName", lst.FirstName);
            cmd.Parameters.AddWithValue("@LastName", lst.LastName);
            cmd.Parameters.AddWithValue("@Residence", lst.Residence);
            cmd.Parameters.AddWithValue("@Phone", lst.Phone);
            cmd.Parameters.AddWithValue("@Time", lst.Time);
            cmd.Parameters.AddWithValue("@Purpose", lst.Purpose);
            cmd.Parameters.AddWithValue("@Meeting", lst.Meeting);
            cmd.Parameters.AddWithValue("@Tag", lst.Tag);
            cmd.Parameters.AddWithValue("@RegDate", lst.RegDate);
            cmd.Parameters.AddWithValue("@Email", lst.Email);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            cmd = new SqlCommand("DeletePGuest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tag", Tag);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdatePGuest(string Date, string Time, string Tag)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Reschedule", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tag", Tag);
            cmd.Parameters.AddWithValue("@RegDate", Date);
            cmd.Parameters.AddWithValue("Time", Time);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}