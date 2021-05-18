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
        public ActionResult Index() { return RedirectToAction("Dashboard"); }

        public List<Main> getVisitors()
        {
            VisitorDB vdb = new VisitorDB();
            ModelState.Clear();
            return vdb.GetVisitorList();
        }
        public List<Main> getPGuests()
        {
            GuestDB vdb = new GuestDB();
            ModelState.Clear();
            return vdb.GetPGuests();
        }
        public List<Main> getCGuests()
        {
            GuestDB vdb = new GuestDB();
            ModelState.Clear();
            return vdb.GetCGuests();
        }

        [HttpPost]
        public ActionResult addVisitor(Main zmodel)
        {
            VisitorDB vdb = new VisitorDB();
            vdb.AddNewVisitor(zmodel);
            ViewBag.Message = "Done!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult addGuest(Main zmodel, Welcome qmodel)
        {
            GuestDB vdb = new GuestDB();

            if (qmodel.DOB != null) { zmodel.RegDate = Convert.ToDateTime(Convert.ToString(qmodel.DOB)).Date.ToString("dd/MM/yyyy"); } else { return RedirectToAction("Appointment", "Welcome"); }

            if (zmodel.STime != null) { zmodel.STime = Convert.ToDateTime(qmodel.Time.ToString("t")); } else { return RedirectToAction("Appointment", "Welcome"); }

            vdb.AddNewGuest(zmodel);
            Main.glalert = 1;
            return RedirectToAction("Index", "Welcome");
        }

        [HttpPost]
        public ActionResult checkOut(string Tag)
        {
            VisitorDB vdb = new VisitorDB();
            vdb.CheckOutVisitor(Tag);
            return RedirectToAction("Registry");
        }
        [HttpPost]
        public ActionResult recordGuest(string Tag)
        {
            GuestDB gdb = new GuestDB();
            gdb.RecordGuest(Tag);
            return RedirectToAction("Registry");
        }

        [HttpPost]
        public void getTag(string Tag)
        {
            ViewBag.usertag = Tag;
        }

        [HttpPost]
        public ActionResult confirmGuest(string Tag)
        {
            GuestDB gdb = new GuestDB();
            gdb.ConfirmGuest(Tag);
            return RedirectToAction("GuestList");
        }
        [HttpPost]
        public ActionResult removeFilter()
        {
            Main.FilterStatus = false;
            return RedirectToAction("Registry");
        }

        [HttpPost]
        public ActionResult Reschedule(Main model)
        {
            GuestDB gdb = new GuestDB();
            gdb.UpdatePGuest(Convert.ToDateTime(model.DOB).ToString("d"), model.STime.ToString("t"), model.Tag);

            return RedirectToAction("GuestList");
        }

        [HttpPost]
        public ActionResult setFilter(Main zmodel)
        {
            List<Main> FilteredList = new List<Main>();

            IEnumerable<Main> check = new List<Main>();
            IEnumerable<Main> result = getVisitors();

            if (zmodel.FirstName != null) { check = result.Where(model => model.FirstName == zmodel.FirstName); result = check; }
            if (zmodel.LastName != null) { check = result.Where(model => model.LastName == zmodel.LastName); result = check; }
            if (zmodel.Residence != null) { check = result.Where(model => model.Residence == zmodel.Residence); result = check; }
            if (zmodel.Phone != null) { check = result.Where(model => model.Phone == zmodel.Phone); result = check; }
            if (zmodel.STimeFrom != null
                && zmodel.STimeTo != null)
            {
                check = result.Where(model => Convert.ToDateTime(model.Check_In) >= Convert.ToDateTime(zmodel.STimeFrom)
                && Convert.ToDateTime(model.Check_Out != "" ? model.Check_Out : DateTime.Now.ToString("t")) <= Convert.ToDateTime(zmodel.STimeTo)); result = check;
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
            Main.FilterList = FilteredList;
            Main.FilterStatus = true;
            return RedirectToAction("Registry");
        }

        public List<Main> defaultFilter()
        {
            List<Main> FilteredList = new List<Main>();

            IEnumerable<Main> check = new List<Main>();
            IEnumerable<Main> result = getVisitors();

            check = result.Where(model => Convert.ToDateTime(model.RegDate) >= DateTime.Now.AddDays(-30)
                && Convert.ToDateTime(model.RegDate) <= DateTime.Now);

            foreach (var entry in check)
            {
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

            return FilteredList;
        }

        // GET: Main
        public ActionResult Dashboard()
        {
            Main.VisitorList = getVisitors();
            Main.CGuestList = getCGuests();
            Main mod = new Main();
            Main.RatingsList = mod.GetRatings();
            return View();
        }
        public ActionResult Registry()
        {
            if (Main.FilterStatus) { Main.VisitorList = Main.FilterList; }
            else { Main.VisitorList = defaultFilter(); }
            return View();
        }
        public ActionResult GuestList()
        {
            Main.glalert = 0;
            Main.VisitorList = getVisitors();
            Main.CGuestList = getCGuests();
            Main.PGuestList = getPGuests();
            return View();
        }
        public ActionResult Recorder()
        {
            return View();
        }
        public ActionResult Staff()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Filter()
        {
            Main.VisitorList = getVisitors();
            return View();
        }
    }
}