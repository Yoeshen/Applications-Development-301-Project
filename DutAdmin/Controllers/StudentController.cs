using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DutAdmin.Models;
using System.Net;
using System.Threading.Tasks;

namespace DutAdmin.Controllers
{
    public class StudentController : Controller
    {

        [ActionName("CreateStudent")]
        public async Task<ActionResult> CreateStudentAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateStudent")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStudentAsync([Bind(Include = "StudentNo,FirstName,LastName,Email,HomeAddress,Mobile,StudentPhoto,IsActive")] Student item)
        {
            if (ModelState.IsValid)
            {
                await DBRepository<Student>.CreateStudentAsync(item);
                return RedirectToAction("Index");
            }
            return View (item);
        }

        [HttpPost]
        [ActionName("EditStudent")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStudentAsync([Bind(Include = "StudentNo,FirstName,LastName,Email,HomeAddress,Mobile,StudentPhoto,IsActive")] Student item)
        {
            if (ModelState.IsValid)
            {
                await DBRepository<Student>.UpdateStudentAsync(item.StudentNo, item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [ActionName("EditStudent")]
        public async Task<ActionResult> EditStudentAsync(string studentno)
        {
            if(studentno == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student item = await DBRepository<Student>.GetStudentAsync(studentno);
        }

        // GET: Student
        public ActionResult Index()
        { 
            return View();


        }
    }
}