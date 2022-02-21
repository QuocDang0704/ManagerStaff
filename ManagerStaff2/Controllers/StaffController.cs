using ManagerStaff2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ManagerStaff2.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            StaffList staffList = new StaffList();
            List<Staff> lst = staffList.getStaff(String.Empty).OrderBy(x => x.name).ToList();
            return View(lst.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Staff st)
        {
            if (ModelState.IsValid)
            {
                StaffList newStaff = new StaffList();
                newStaff.AddStaff(st);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(String id= "")
        {
            StaffList newStaff = new StaffList();
            List<Staff> lst = newStaff.getStaff(id);
            return View(lst.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Staff staff)
        {
            StaffList newStaff = new StaffList();
            newStaff.UpdateStaff(staff);
            return RedirectToAction("Index");
        }
        public ActionResult Details(String id = "")
        {
            StaffList newStaff = new StaffList();
            List<Staff> lst = newStaff.getStaff(id);
            return View(lst.FirstOrDefault());
        }
        public ActionResult Delete(String id = "")
        {
            StaffList newStaff = new StaffList();
            List<Staff> lst = newStaff.getStaff(id);
            return View(lst.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(Staff staff)
        {
            StaffList newStaff = new StaffList();
            newStaff.DeleteStaff(staff);
            return RedirectToAction("Index");
        }
    }
}
