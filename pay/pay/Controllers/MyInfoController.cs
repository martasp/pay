using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pay.Data;
using pay.Models;
using pay.Models.ViewModels;
using pay.Services;
using Microsoft.AspNetCore.Authorization;

namespace pay.Controllers
{
    [Authorize()]
    public class MyInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var CurrentUserID = User.Identity.Name;
            var user = await _context.User.SingleOrDefaultAsync(m => m.OwnerID == CurrentUserID);

            if (user == null)
            {
                return View("Create");
            }

            var userViewModel = new UserViewModel
            {
                ID = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                NetSalary = user.Salary,
                GroosSalary = NetGrossCalculator.CalculateGross(user.Salary)
            };
            return View("Details", userViewModel);
        }

        // GET: Users/Details
        public async Task<IActionResult> Details()
        {
            var CurrentUserID = User.Identity.Name;
            var user = await _context.User.SingleOrDefaultAsync(m => m.OwnerID == CurrentUserID);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = new UserViewModel
            {
                ID = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                NetSalary = user.Salary,
                GroosSalary = NetGrossCalculator.CalculateGross(user.Salary)
            };
            return View(userViewModel);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var CurrentUserID = User.Identity.Name;
            var user = await _context.User.SingleOrDefaultAsync(m => m.OwnerID == CurrentUserID);
            if (user == null)
            {
                return View("Create");
            }
            return View("Index");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Salary")] User user)
        {
            if (ModelState.IsValid)
            {
                user.OwnerID = User.Identity.Name;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit
        public async Task<IActionResult> Edit()
        {
            var CurrentUserID = User.Identity.Name;
            var user = await _context.User.SingleOrDefaultAsync(m => m.OwnerID == CurrentUserID);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Surname,Salary")] User user)
        {
            if (ModelState.IsValid)
            {
                var CurrentUserID = User.Identity.Name;
                var userk = await _context.User.SingleOrDefaultAsync(m => m.OwnerID == CurrentUserID);
                userk.Name = user.Name;
                userk.Surname = user.Surname;
                userk.Salary = user.Salary;
                _context.Update(userk);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
