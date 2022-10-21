using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIBKMNET_API.Context;
using SIBKMNET_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        MyContext myContext;

        public UserController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = myContext.User.ToList();
            return Ok(new { status = 200, message = "data has been collected", data = data });
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var data = myContext.User.Find(id);
            return Ok(new { status = 200, message = "data has been collected", data = data });
        }

        [HttpPost]
        public ActionResult Post(ViewModels.UserVM userVM)
        {
            User user = new User(userVM);
            myContext.User.Add(user);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { status = 200, message = "data has been inserted" });
            }
            return BadRequest(new { status = 400, message = "data has not been inserted" });
        }

        [HttpPut]
        public ActionResult Put(ViewModels.UserVM userVM)
        {
            User user = new User(userVM);
            //myContext.Employees.Update(employee);
            myContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { status = 200, message = "data has been updated" });
            }
            return BadRequest(new { status = 400, message = "data has not been updated" });

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var data = myContext.User.Find(id);
            myContext.User.Remove(data);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return Ok(new { status = 200, message = "data has been deleted" });
            }
            return BadRequest(new { status = 400, message = "data has not been deleted" });
        }
    }
}
