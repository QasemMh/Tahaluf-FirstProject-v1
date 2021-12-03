using Iam_Influencer.Models;
using Iam_Influencer.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ModelContext _context;
        readonly IWebHostEnvironment _hostEnvironment;
        public AuthenticationController(ModelContext context, IWebHostEnvironment host)
        {
            _context = context;
            _hostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(Login user)
        {


            const string USERNAME = "username";
            // 3=> customer  1=>admin  2=>accountant

            if (ModelState.IsValid)
            {

                var Authen = _context.Logins.
                    Where(x => x.Username.ToLower() == user.Username.ToLower() &&
                    x.Password == user.Password).SingleOrDefault();

                if (Authen != null)
                {
                    switch (Authen.RoleId)
                    {
                        //3 >> customer
                        case 3:
                            int customerId = (int)_context.Customers.Where(c => c.Id == Authen.CustomerId).FirstOrDefault().Id;
                            HttpContext.Session.SetString(USERNAME, Authen.Username);
                            HttpContext.Session.SetInt32("customer", (int)customerId);
                            HttpContext.Session.SetInt32("roleid", (int)Authen.RoleId);
                            return RedirectToAction("Index", "Customers");

                        case 2:
                            HttpContext.Session.SetString(USERNAME, Authen.Username);
                            HttpContext.Session.SetInt32("roleid", (int)Authen.RoleId);
                            HttpContext.Session.SetInt32("employee", (int)Authen.AccountatntId);
                            return RedirectToAction("Index", "Employees");

                        case 1:
                            //1 >> Admin
                            HttpContext.Session.SetString(USERNAME, Authen.Username);
                            HttpContext.Session.SetInt32("roleid", (int)Authen.RoleId);
                            HttpContext.Session.SetInt32("admin", (int)Authen.Id);
                            return RedirectToAction("Index", "Admin");

                        default: return View(user);

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Password or Username Invaild");
                    return View(user);
                }
            }

            return View(user);
        }


        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UsersViewModel user)
        {
            user.Username = user.Username.ToLower();
            if (IsUsernameExists(user.Username))
            {
                ModelState.AddModelError("Username", "Username Is Already Exists");
                return View(user);
            }

            if (user.Email != null)
            {
                user.Email = user.Email.ToLower();

                if (IsEmailExists(user.Email))
                {
                    ModelState.AddModelError("Email", "Email Is Already Exists");
                    return View(user);
                }
            }
            Customer customer = new Customer();

            if (ModelState.IsValid)
            {
                var img = user.Image;
                if (img != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                    //save omage name in db
                    user.ImagePath = fileName;
                    var path = Path.Combine(wwwRootPath + "/images/" + fileName);
                    //save image in server
                    //System.IO.File.Create(path);
                    using (var sf = new FileStream(path, FileMode.Create))
                    {
                        await img.CopyToAsync(sf);
                    }
                }

                //create user
                customer.Firstname = user.FirstName;
                customer.Lastname = user.LastName;
                customer.Imagepath = user.ImagePath;
                // customer.AddressId = lastAddressId;
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                var LastId = _context.Customers.OrderByDescending(p =>
                p.Id).FirstOrDefault().Id;

                Login account = new Login();
                account.Email = user.Email;
                account.Username = user.Username;
                account.Password = user.Password;
                account.CustomerId = LastId;
                account.RoleId = 3;//customer
                _context.Logins.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                return View(user);
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Login));
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
