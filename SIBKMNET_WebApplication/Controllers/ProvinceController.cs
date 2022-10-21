using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIBKMNET_WebApplication.Context;
using SIBKMNET_WebApplication.Models;
using SIBKMNET_WebApplication.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

namespace SIBKMNET_WebApplication.Controllers
{
    public class ProvinceController : Controller
    {
        ProvinceRepository ProvinceRepository;

        public ProvinceController(ProvinceRepository ProvinceRepository)
        {
            this.ProvinceRepository = ProvinceRepository;
        }

        public IActionResult Index()
        {
            var Role = HttpContext.Session.GetString("Role");
            if (Role.Equals("Admin") && Role != null)
            {
                var data = ProvinceRepository.Get();

                return View(data);
            }
            //return RedirectToAction("Index", "Region");
            return RedirectToAction("Unauthorized", "ErrorPage");
            
        }


        //getid / detail
        public IActionResult Details(int id)
        {
            var data = ProvinceRepository.Get(id);

            return View(data);
        }

        //create
        [HttpGet]
        public IActionResult Create()
        {
            //var data = myContext.Provinces.Include(x => x.Region).FirstOrDefault(x => x.Id.Equals(id));
            //var data = myContext.Provinces.Find(id);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Province province)
        {
            if (ModelState.IsValid)
            {

                
                var result = ProvinceRepository.Post(province);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }


        [HttpGet,ActionName("Edit")]
        public IActionResult Edit(int? id)
        {
            //var data = myContext.Provinces.Include(x => x.Region).FirstOrDefault(x => x.Id.Equals(id));
            //var data = ProvinceRepository.Find(id);
            return View();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Province province)
        {
            
            if (ModelState.IsValid)
            {

                var result = ProvinceRepository.Put(id, province);
                
                
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        //delete
        public IActionResult Delete(Province province)
        {

            
                if (ModelState.IsValid)
                {
                    
                    var result = ProvinceRepository.Delete(province);

                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    

                }

            return View();
        }
    }
}
