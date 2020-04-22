﻿using System;
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
#pragma warning disable 1998
        [ActionName("CreateStudent")]
        public async Task<ActionResult> CreateStudentAsync()
        {
            return View();
        }
#pragma warning disable 1998

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
            if(item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [ActionName("DeleteStudent")]
        public async Task<ActionResult> DeleteStudentAsync(string studentno)
        {
            if(studentno == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student item = await DBRepository<Student>.GetStudentAsync(studentno);
            if(item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStudentConfirmedAsync([Bind(Include = "StudentNo")]string studentno)
        {
            await DBRepository<Student>.DeleteStudentAsync(studentno);
            return RedirectToAction("Index");
        }

        [ActionName("StudentDetails")]
        public async Task<ActionResult> StudentDetailsAsync(string studentno)
        {
            Student item = await DBRepository<Student>.GetStudentAsync(studentno);
            return View(item);
        }

        // GET: Student
        public ActionResult Index()
        { 
            return View();


        }
    }
}