using Iam_Influencer.Models;
using Iam_Influencer.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        readonly IWebHostEnvironment _hostEnvironment;
        public AdminController(ModelContext context, IWebHostEnvironment host)
        {
            _context = context;
            _hostEnvironment = host;
        }

        public IActionResult Index()
        {
            if (!HttpContext.Session.GetInt32("admin").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }

            ViewBag.product = _context.Products.ToList().Count();
            ViewBag.customer = _context.Customers.ToList().Count();
            ViewBag.order = _context.Orederdetails.ToList().Count();
            ViewBag.emp = _context.Employees.ToList().Count();




            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var users = await _context.Logins.Where(c => c.CustomerId != null).ToListAsync();
            var usersViews =
                users.Select(u => new UsersViewModel
                {
                    Username = u.Username,
                    Email = u.Email,
                    RoleName = _context.Roles.ToList().Find(r => r.Id == u.RoleId).Name,
                    Id = (long)u.CustomerId
                });

            return View(usersViews);
        }
        [HttpGet]
        public async Task<IActionResult> AllEmployee()
        {
            var employees = await _context.Logins.Where(a => a.AccountatntId != null).ToListAsync();
            var usersViews =
               employees.Select(u => new UsersViewModel
               {
                   Username = u.Username,
                   Email = u.Email,
                   RoleName = _context.Roles.ToList().Find(r => r.Id == u.RoleId).Name,
                   Id = u.Id
               });
            return View(usersViews);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(UsersViewModel user)
        {
            if (IsUsernameExists(user.Username))
            {
                ModelState.AddModelError("Username", "Username Is Already Exists");
                return View(user);
            }

            if (user.Email != null)
                if (IsEmailExists(user.Email))
                {
                    ModelState.AddModelError("Email", "Email Is Already Exists");
                    return View(user);
                }

            Employee employee = new Employee();

            if (ModelState.IsValid)
            {

                //create user
                employee.Firstname = user.FirstName;
                employee.Lastname = user.LastName;
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                var LastId = _context.Employees.OrderByDescending(p =>
                p.Id).FirstOrDefault().Id;

                Login account = new Login();
                account.Email = user.Email;
                account.Username = user.Username;
                account.Password = user.Password;
                account.AccountatntId = LastId;
                account.RoleId = 2;//employee acc
                _context.Logins.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AllEmployee));
            }
            else
            {
                return View(user);
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query =
                   from users in _context.Logins
                   join emp in _context.Employees on users.AccountatntId equals emp.Id
                   where users.Id == id
                   select new { User = users, Emp = emp };


            var data = await query.FirstOrDefaultAsync();
            if (data == null) return NotFound();

            var user = data.User;
            var employee = data.Emp;

            var userViewModel = new EmployeeViewModel
            {
                Id = user.Id,
                FirstName = employee.Firstname,
                LastName = employee.Lastname,
                Username = user.Username,
                Email = user.Email
            };

            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(long? id,
             EmployeeViewModel employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Logins.Where(e => e.Id == id).FirstOrDefaultAsync();

                    if (user.Username != employee.Username)
                        if (IsUsernameExists(employee.Username))
                        {
                            ModelState.AddModelError("Username", "Username Is Already Exists");
                            return View(employee);
                        }

                    if (user.Email != null)
                        if (user.Email != employee.Email)
                            if (IsEmailExists(employee.Email))
                            {
                                ModelState.AddModelError("Email", "Email Is Already Exists");
                                return View(employee);
                            }


                    user.Username = employee.Username;
                    user.Email = employee.Email;

                    var emp = new Employee
                    {
                        Firstname = employee.FirstName,
                        Lastname = employee.LastName,
                        Id = (long)user.AccountatntId
                    };
                    _context.Employees.Update(emp);
                    _context.Logins.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new Exception();
                }
                return RedirectToAction(nameof(AllEmployee));
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> ManageRoles(Role role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Roles.AddAsync(role);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ManageRoles));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Id", "Id is already exists");
                    return View(_context.Roles.ToListAsync().Result);

                }
            }
            else
            {
                return View(_context.Roles.ToListAsync().Result);
            }
        }





        private bool IsUsernameExists(string username)
        {
            return _context.Logins.Any(u => u.Username == username);
        }
        private bool IsEmailExists(string email)
        {
            return _context.Logins.Any(u => u.Email == email);
        }
    }
}
