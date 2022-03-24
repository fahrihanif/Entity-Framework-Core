using API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Base
{
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        //Get Data
        [Authorize(Roles = "Director, Manager")]
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var get = repository.Get().Count();
                return get == 0
                    ? NotFound(new { message = "Data Tidak Ada" })
                    : (ActionResult)Ok(repository.Get());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Get Data with parameter primary key
        //[Authorize(Roles = "Director, Manager")]
        [AllowAnonymous]
        [HttpGet("Search")]
        [Route("")]
        public ActionResult GetById(Key id)
        {
            try
            {
                var get = repository.Get(id);
                return get == null
                    ? NotFound(new { message = "Data Tidak Ditemukan" })
                    : (ActionResult)Ok(get);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        //Insert data to entity
        [Authorize(Roles = "Director")]
        [HttpPost]
        public virtual ActionResult Post(Entity entity)
        {
            try
            {
                var post = repository.Insert(entity);
                return post == 0
                    ? NotFound(new { message = "Data Gagal Disimpan Silahkan Periksa Kembali" })
                    : (ActionResult)Ok(new { message = "Data Berhasil Disimpan" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Update data from existing row in entity
        [AllowAnonymous]
        [HttpPut]
        public virtual ActionResult Update(Entity entity)
        {
            try
            {
                var update = repository.Update(entity);
                return update == 0
                    ? NotFound((new { message = "Data Gagal Diubah Silahkan Periksa Kembali" }))
                    : (ActionResult)Ok(new { message = "Data Berhasil Diubah" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Delete data from row in entity by primary key
        //[Authorize]
        [AllowAnonymous]
        [HttpDelete("Delete")]
        [Route("")]
        public ActionResult Delete(Key id)
        {
            try
            {
                var delete = repository.Delete(id);
                return delete == 0
                    ? NotFound(new { message = $"{id} Not Found" })
                    : (ActionResult)Ok(new { message = $"Your selected id has been deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
