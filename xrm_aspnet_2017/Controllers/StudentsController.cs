﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using xrm_aspnet_2017.Data;
using xrm_aspnet_2017.Models;

namespace xrm_aspnet_2017.Controllers {
    public class StudentsController : Controller {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context) {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index() {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var student = await _context.Students

              .Include(s => s.Enrollments)
              .ThenInclude(e => e.Course)
              .AsNoTracking()
              .SingleOrDefaultAsync(m => m.ID == id);

            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(

            [Bind("EnrollmentDate,FirstMidName,LastName")] Student student) {

            try {
                if (ModelState.IsValid) {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            catch (DbUpdateException /* ex */) {
                //Log the error 
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        // GET: Students/Edit/{id}
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null) {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/{id}
        // Scaffolded method replaced to provide 'security best practices
        // to prevent overposting' (removed binder)
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) {
            if (id == null) {
                return NotFound();
            }
            var studentToUpdate = await _context.Students.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate)) {
                try {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */) {
                    //Log the error 
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // Scaffolded [HttpPost] Edit
        // Modified to catch all exceptions, not just DbUpdateConcurrencyException

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,EnrollmentDate,FirstMidName,LastName")] Student student) {
        //    if (id != student.ID) {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid) {
        //        try {
        //            _context.Update(student);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (DbUpdateException ex) {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //    }
        //    return View(student);
        //}



        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.ID == id);
            if (student == null) {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var student = await _context.Students
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (student == null) {
                return RedirectToAction(nameof(Index));
            }

            try {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */) {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        // Scaffolded Delete 
        // POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id) {
        //    var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
        //    _context.Students.Remove(student);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool StudentExists(int id) {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}