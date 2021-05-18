using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProtoTwo.Models
{
    public class Welcome
    {
        public static IList<Main> RatingsList;

        private SqlConnection conn;
        private void connection()
        {
            string connstring = ConfigurationManager.ConnectionStrings["VMSDbConn"].ToString();
            conn = new SqlConnection(connstring);
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public void SaveRating(string Rating)
        {
            connection();
            List<Main> UserList = new List<Main>();

            SqlCommand cmd = new SqlCommand("SaveRating", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Rating", Rating);
            cmd.Parameters.AddWithValue("@SetDate", DateTime.Now.ToString("d"));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}