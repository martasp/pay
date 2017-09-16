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
using Microsoft.AspNetCore.Authorization;
using pay.Services;
using pay.Repository;
namespace pay.Controllers
{

    public class EmployersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployerRepository _repository;
        public EmployersController(ApplicationDbContext context, EmployerRepository repository )
        {
            _context = context;
            _repository = repository;
        }

        // GET: Users
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index(int id)
        {
            Pagination(id);
            var users = await _repository.GetAll(id).ToListAsync();// await _context.User.Skip(id * 15).Take(15).ToListAsync();

            var UsersView = users.Select(x => new UserViewModel
            {
                ID = x.UserId,
                Name = x.Name,
                Surname = x.Surname,
                NetSalary = x.Salary,
                GroosSalary = NetGrossCalculator.CalculateGross(x.Salary)
            }).ToList();
            return View("Index", UsersView);
        }

        private void Pagination(int id)
        {
                ViewData["up"] = id + 1;
                ViewData["down"] = id - 1;
                 if (Convert.ToInt64(ViewData["down"]) == -1 )
                 {
                    ViewData["down"] = 0;
                 }
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,OwnerID,Name,Surname,Salary")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}

