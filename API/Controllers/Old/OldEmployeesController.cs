using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public OldEmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //Get Data
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var get = employeeRepository.Get().Count();
                return get == 0 
                    ? NotFound(new { msg = "Data Employee Tidak Ada" }) 
                    : (ActionResult) Ok(get);
            }
            catch
            {
                return BadRequest(new { msg = "Error" });
            }
        }

        //Get Data with parameter
        [HttpGet("Search")]
        [Route("")]
        public ActionResult GetById(string NIK)
        {
            try
            {
                var get = employeeRepository.Get(NIK);
                return get == null 
                    ? NotFound(new { msg = "Data Tidak Ditemukan" }) 
                    : (ActionResult) Ok(get);
            }
            catch
            {
                return BadRequest(new { msg = "Error" });
            }
            
        }

        //Insert
        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            try
            {
                var post = employeeRepository.Insert(employee);
                return post == 0
                    ? NotFound(new { msg = "Data Gagal Disimpan Silahkan Periksa Email dan Phone Number" })
                    : (ActionResult)Ok(new { msg = "Data Berhasil Disimpan" });
            }
            catch
            {
                return BadRequest(new { msg = "Error" });
            }
        }

        //Update
        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            try
            {
                var update = employeeRepository.Update(employee);
                return update == 0
                    ? NotFound((new { msg = "Data Phone Number dan Email Tidak Boleh Sama Dengan Yang Lain" }))
                    : (ActionResult)Ok(new { msg = "Data Berhasil Diubah" });
            }
            catch
            {
                return BadRequest(new { msg = "Error" });
            }
        }

        //Delete
        [HttpDelete("Delete")]
        [Route("")]
        public ActionResult Delete(string NIK)
        {
            try
            {
                var delete = employeeRepository.Delete(NIK);
                return delete == 0 
                    ? NotFound(new { msg = "NIK Tidak Ditemukan" }) 
                    : (ActionResult)Ok(new { msg = $"Data {NIK} Berhasil Dihapus" });
            }
            catch
            {
                return BadRequest(new { msg = "Error" });
            }
        }
    }
}
