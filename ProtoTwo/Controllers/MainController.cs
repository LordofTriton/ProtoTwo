using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProtoTwo.Models;

namespace ProtoTwo.Controllers
{
    public class MainController : Controller
    {
        //Tried to name the default action method 'Dashboard'. Didn't work. So I did this :)
        public ActionResult Index() { return RedirectToAction("Dashboard"); }

        //Get complete list of Visitors from Database
        public List<Main> getVisitors()
        {
            VisitorDB vdb = new VisitorDB(); // << Specialized class for handling Visitors Database functions...
            ModelState.Clear();
            return vdb.GetVisitorList();
        }
        //Get complete list of Pending Appointments from Database
        public List<Main> getPGuests()
        {
            GuestDB vdb = new GuestDB(); // << Specialized class for handling Guests Database functions...
            ModelState.Clear();
            return vdb.GetPGuests();
        }
        //Get complete list of Pending Appointments from Database
        public List<Main> getCGuests()
        {
            GuestDB vdb = new GuestDB();
            ModelState.Clear();
            return vdb.GetCGuests();
        }

        //Add Visitor to the Database
        [HttpPost]
        public ActionResult addVisitor(Main zmodel)
        {
            VisitorDB vdb = new VisitorDB();
            vdb.AddNewVisitor(zmodel);
            return RedirectToAction("Index");
        }
        //Add Guests to the Database
        [HttpPost]
        public ActionResult addGuest(Main zmodel, Welcome qmodel)
        {
            GuestDB vdb = new GuestDB();

            //Check if the user supplied Date and Time in the Schedule Appointment Page.
            //The error has been fixed by making the fields required, so these are pointless...
            if (qmodel.DOB != null) { zmodel.RegDate = Convert.ToDateTime(Convert.ToString(qmodel.DOB)).Date.ToString("dd/MM/yyyy"); } else { return RedirectToAction("Appointment", "Welcome"); }
            if (zmodel.STime != null) { zmodel.STime = Convert.ToDateTime(qmodel.Time.ToString("t")); } else { return RedirectToAction("Appointment", "Welcome"); }

            vdb.AddNewGuest(zmodel);
            Main.glalert = 1; // << Add a 'Guest Page' notification
            return RedirectToAction("Index", "Welcome");
        }

        //Update 'Check Out' time for a selected Visitor
        [HttpPost]
        public ActionResult checkOut(string Tag)
        {
            VisitorDB vdb = new VisitorDB();
            vdb.CheckOutVisitor(Tag);
            return RedirectToAction("Registry");
        }

        //Record a Guest. Moves entries from the Confirmed Guest List to the Visitors Registry
        [HttpPost]
        public ActionResult recordGuest(string Tag)
        {
            GuestDB gdb = new GuestDB();
            gdb.RecordGuest(Tag);
            return RedirectToAction("Registry");
        }

        //Confirm an Appointment. Copies an entry from the Pending Guest List to the Confirmed Guest List
        //Deletes the original entry in the Pending Guest List.
        [HttpPost]
        public ActionResult confirmGuest(string Tag)
        {
            GuestDB gdb = new GuestDB();
            gdb.ConfirmGuest(Tag);
            return RedirectToAction("GuestList");
        }

        //Sets Filter Status to false. Filter Status determines whether the Visitors Registry reflects the Filtered List or not.
        [HttpPost]
        public ActionResult removeFilter()
        {
            Main.FilterStatus = false;
            return RedirectToAction("Registry");
        }

        //Reschedule Pending Appointment to user provided Date and Time
        [HttpPost]
        public ActionResult Reschedule(Main model)
        {
            GuestDB gdb = new GuestDB();
            gdb.UpdatePGuest(Convert.ToDateTime(model.DOB).ToString("d"), model.STime.ToString("t"), model.Tag);

            return RedirectToAction("GuestList");
        }

        //Enforce Filter
        //Filters the entries in the displayed Visitors List by user provided values...
        [HttpPost]
        public ActionResult setFilter(Main zmodel)
        {
            List<Main> FilteredList = new List<Main>(); // << Create blank FilteredList ready to recieve values

            IEnumerable<Main> check = new List<Main>();
            //Had to create a separate List because  'check = check.Where(mode....'  DIDN'T WORK. Even though it should.
            IEnumerable<Main> result = getVisitors();

            //Filter Engine :)
            if (zmodel.FirstName != null) { check = result.Where(model => model.FirstName == zmodel.FirstName); result = check; }
            if (zmodel.LastName != null) { check = result.Where(model => model.LastName == zmodel.LastName); result = check; }
            if (zmodel.Residence != null) { check = result.Where(model => model.Residence == zmodel.Residence); result = check; }
            if (zmodel.Phone != null) { check = result.Where(model => model.Phone == zmodel.Phone); result = check; }
            if (zmodel.STimeFrom != null
                && zmodel.STimeTo != null)
            {
                check = result.Where(model => Convert.ToDateTime(model.Check_In) >= Convert.ToDateTime(zmodel.STimeFrom) && Convert.ToDateTime(model.Check_Out != "" ? model.Check_Out : DateTime.Now.ToString("t")) <= Convert.ToDateTime(zmodel.STimeTo)); result = check;
            }
            if (zmodel.Purpose != null) { check = result.Where(model => model.Purpose == zmodel.Purpose); result = check; }
            if (zmodel.Meeting != null) { check = result.Where(model => model.Meeting == zmodel.Meeting); result = check; }
            if (zmodel.Tag != null) { check = result.Where(model => model.Tag == zmodel.Tag); result = check; }
            if (zmodel.DateFrom != null
                && zmodel.DateTo != null) { check = result.Where(model => Convert.ToDateTime(model.RegDate) >= Convert.ToDateTime(zmodel.DateFrom)
                && Convert.ToDateTime(model.RegDate) <= Convert.ToDateTime(zmodel.DateTo)); result = check; }
            if (zmodel.Email != null) { check = result.Where(model => model.Email == zmodel.Email); result = check; }

            foreach (var entry in check)
            {
                //Parse the filtered results into the prepared list...
                FilteredList.Add(
                    new Main
                    {
                        FirstName = entry.FirstName,
                        LastName = entry.LastName,
                        Residence = entry.Residence,
                        Phone = entry.Phone,
                        Check_In = entry.Check_In,
                        Check_Out = entry.Check_Out,
                        Purpose = entry.Purpose,
                        Meeting = entry.Meeting,
                        Tag = entry.Tag,
                        RegDate = entry.RegDate,
                        Email = entry.Email
                    });
            }
            Main.FilterList = FilteredList; // << Update VisitorList
            Main.FilterStatus = true; // << Set FilterStatus to true. :/
            return RedirectToAction("Registry");
        }

        //Enforce Default Filter
        //Limits the entries in the displayed Visitors List to entries made in the last 30 days.
        public List<Main> defaultFilter()
        {
            List<Main> FilteredList = new List<Main>(); // << Create blank FilteredList ready to recieve values

            IEnumerable<Main> check = new List<Main>();
            //Had to create a separate List because  'check = check.Where(mode....'  DIDN'T WORK. Even though it should.
            IEnumerable<Main> result = getVisitors();

            //Filter Engine :)
            check = result.Where(model => Convert.ToDateTime(model.RegDate) >= DateTime.Now.AddDays(-30)
                && Convert.ToDateTime(model.RegDate) <= DateTime.Now);

            foreach (var entry in check)
            {
                //Parse the filtered results into the prepared list...
                FilteredList.Add(
                    new Main
                    {
                        FirstName = entry.FirstName,
                        LastName = entry.LastName,
                        Residence = entry.Residence,
                        Phone = entry.Phone,
                        Check_In = entry.Check_In,
                        Check_Out = entry.Check_Out,
                        Purpose = entry.Purpose,
                        Meeting = entry.Meeting,
                        Tag = entry.Tag,
                        RegDate = entry.RegDate,
                        Email = entry.Email
                    });
            }

            return FilteredList; //Return list
        }

        // GET: Main
        //Defacto Index Page :)
        //Displays various information and links to other pages
        // myite/plce/plce
        public ActionResult Dashboard()
        {
            //Refresh Lists
            Main.VisitorList = getVisitors();
            Main.CGuestList = getCGuests();
            Main.PGuestList = getPGuests();

            Main mod = new Main(); // << new instance of Main :(
            Main.RatingsList = mod.GetRatings(); // << Get Customer Satisfaction Ratings. Roundabout solution. Can be optimised.
            return View();
        }

        //Visitors Registry
        //Displays the contents of the Visitors Database with default or custom filter.
        public ActionResult Registry()
        {
            //Refresh Lists
            Main.CGuestList = getCGuests();
            Main.PGuestList = getPGuests();

            //Refresh Visitor List depending on FilterStatus status
            if (Main.FilterStatus) { Main.VisitorList = Main.FilterList; }
            else { Main.VisitorList = defaultFilter(); }
            return View();
        }

        //Guest List
        //Displays the contents of the Guest Databases. WITHOUT FILTER. Need to add filter.
        public ActionResult GuestList()
        {
            Main.glalert = 0; // << Clears Guest Page notification

            //Refresh Lists
            Main.VisitorList = getVisitors();
            Main.CGuestList = getCGuests();
            Main.PGuestList = getPGuests();

            return View();
        }

        //Recorder
        //Contain the function(s) for adding entries to the Guest List
        public ActionResult Recorder()
        {
            return View();
        }

        //Staff (Renamed 'PhoneBook' in the View)
        //Displays all important or representative personnel in the company. This page might be removed soon.
        public ActionResult Staff()
        {
            return View();
        }

        //About Page
        //Displays general information about the solution and Visitor Management. This page might be removed soon.
        public ActionResult About()
        {
            return View();
        }

        //Filter
        //Contains the Filter function for filtering the VISITORS LIST by user provided parameters.
        public ActionResult Filter(Main model)
        {
            //Attempts to clear these values from the model class. 
            // They retain values from previous filter operations, causing errors.
            model.STimeFrom = Convert.ToDateTime("01/01/0001"); 
            model.STimeTo = DateTime.Now;
            ModelState.Clear();

            Main.VisitorList = getVisitors(); // << Refresh Visitor List to prepare for Filtering.
            return View(model);
        }
    }
}