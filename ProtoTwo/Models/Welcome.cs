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
        //Initialize List
        public static IList<Main> RatingsList;

        private SqlConnection conn; //Create a connection object
        //Initialize the connection
        private void connection()
        {
            string connstring = ConfigurationManager.ConnectionStrings["VMSDbConn"].ToString(); //The connection string is provided in the 'web.config' file
            conn = new SqlConnection(connstring);
        }

        //Necessary Variables
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        //Saves Ratings into the CSR Db
        public void SaveRating(string Rating)
        {
            connection(); //Init connection

            SqlCommand cmd = new SqlCommand("SaveRating", conn); //Create an SQL Query string using the provided Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Add parameters into the Stored Procedure
            cmd.Parameters.AddWithValue("@Rating", Rating);
            cmd.Parameters.AddWithValue("@SetDate", DateTime.Now.ToString("d"));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}