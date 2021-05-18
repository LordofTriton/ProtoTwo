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
    public class Main
    {
        private SqlConnection conn;
        private void connection()
        {
            string connstring = ConfigurationManager.ConnectionStrings["VMSDbConn"].ToString();
            conn = new SqlConnection(connstring);
        }

        public static IList<Main> VisitorList;
        public static IList<Main> CGuestList;
        public static IList<Main> PGuestList;
        public static IList<Main> UserList;
        public static IList<Main> FilterList;
        public static IList<Main> RatingsList;

        public string UserName { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Residence { get; set; }
        public string Check_In { get; set; }
        public string Check_Out { get; set; }
        public string Purpose { get; set; }
        public string Meeting { get; set; }
        public string Date { get; set; }
        public string Tag { get; set; }
        public string RegDate { get; set; }
        public string Email { get; set; }

        public static string period_filter = "Today";
        public static int count = 1;
        public static bool FilterStatus = false;

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        public static int glalert = 0;
        public int Ratings { get; set; }
        public string SetDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime STime { get; set; }
        [DataType(DataType.Time)]
        public DateTime STimeTo { get; set; }
        [DataType(DataType.Time)]
        public DateTime STimeFrom { get; set; }

        public string Time { get; set; }

        public List<Main> GetRatings()
        {
            connection();
            List<Main> RatingsList = new List<Main>();

            SqlCommand cmd = new SqlCommand("GetRatings", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            sd.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                RatingsList.Add(
                    new Main
                    {
                        Ratings = Convert.ToInt32(dr["Rating"]),
                        SetDate = Convert.ToString(dr["SetDate"])
                    });
            }
            return RatingsList;
        }

        public bool isInWeek(DateTime date0)
        {
            var date = date0.ToString("d");
            DateTime date1 = Convert.ToDateTime(date).Date;

            date = DateTime.Now.ToString("d");
            DateTime date2 = Convert.ToDateTime(date).Date;

            return date1.AddDays(-(int)date1.DayOfWeek).Date == date2.AddDays(-(int)date2.DayOfWeek).Date;
        }
    }
}