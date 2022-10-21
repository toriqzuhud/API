using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIBKMNET_WebApplication.Models;
using SIBKMNET_WebApplication.Repositories.Data;
using SIBKMNET_WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_WebApplication.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            // ambil data dari database sesuai dengan email dan pass
            //if (ModelState.IsValid)
            //{
                var data = accountRepository.Login(login);
                if (data != null)
                {
                    // inisialisasi nilai pada session
                    HttpContext.Session.SetString("Role", data.Role);
                    return RedirectToAction("Index", "Province");
                }
                return View();
            //}
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            var data = accountRepository.Register(register);
            if (data >0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            var data = accountRepository.ForgotPassword(forgotPassword);
            if (data>0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }


        public IActionResult ChangePassword()
        {
            return View();

        }

        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            var data = accountRepository.ChangePassword(changePassword);
            if (data != null)
            {
                // inisialisasi nilai pada session
            }
            return View();
            

        }



    }
}
