using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var get = repository.Get().Count();
                return get == 0
                    ? NotFound(new { msg = "Data Tidak Ada" })
                    : (ActionResult)Ok(repository.Get());
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e });
            }
        }

        //Get Data with parameter
        [HttpGet("Search")]
        [Route("")]
        public ActionResult GetById(Key id)
        {
            try
            {
                var get = repository.Get(id);
                return get == null
                    ? NotFound(new { msg = "Data Tidak Ditemukan" })
                    : (ActionResult)Ok(get);
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e });
            }

        }

        //Insert
        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var post = repository.Insert(entity);
                return post == 0
                    ? NotFound(new { msg = "Data Gagal Disimpan Silahkan Periksa Kembali" })
                    : (ActionResult)Ok(new { msg = "Data Berhasil Disimpan" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e });
            }
        }

        //Update
        [HttpPut]
        public virtual ActionResult Update(Entity entity)
        {
            try
            {
                var update = repository.Update(entity);
                return update == 0
                    ? NotFound((new { msg = "Data Gagal Diubah Silahkan Periksa Kembali" }))
                    : (ActionResult)Ok(new { msg = "Data Berhasil Diubah" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e });
            }
        }

        //Delete
        [HttpDelete("Delete")]
        [Route("")]
        public ActionResult Delete(Key id)
        {
            try
            {
                var delete = repository.Delete(id);
                return delete == 0
                    ? NotFound(new { msg = $"{id} Tidak Ditemukan" })
                    : (ActionResult)Ok(new { msg = $"Id {id} Berhasil Dihapus" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e });
            }
        }
    }
}
