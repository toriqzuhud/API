﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIBKMNET_API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_API.Base.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Repository, Entity> : ControllerBase
        where Repository : class, IGeneralRepository<Entity>
    {

        Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        //CREATE
        public ActionResult Post(Entity entity)
        {
            var result = repository.Post(entity);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();

        }

        //UPdATE

        public ActionResult Put(int id, Entity entity)
        {
            var result = repository.Put(entity);
            if (result>0)
            {
                return Ok();
            }
            return BadRequest();
        }

        // GET

        public ActionResult Get()
        {
            var data = repository.Get();
            return Ok(new { data = data });
        }

        //GET BY ID
        public ActionResult Get(int id)
        {
            var data = repository.Get(id);
            return Ok(new { data = data });
        }

        //DELETE
        public ActionResult Delete(int id)
        {
            var result = repository.Delete(id);
            if (result>0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
