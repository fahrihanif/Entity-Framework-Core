using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //This class to implement BaseController in Employee
    [Authorize]
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }

        //Get Data master
        [Authorize(Roles = "Director, Manager")]
        [HttpGet("Master")]
        public ActionResult GetAllMaster()
        {
            try
            {
                var get = repository.MasterEmployeeData();
                return get == null
                    ? NotFound(new { message = "Data Tidak Ada" })
                    : (ActionResult)Ok(repository.MasterEmployeeData());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Update employee
        [Authorize]
        [HttpPut]
        public override ActionResult Update(Employee entity)
        {
            try
            {
                var update = repository.Update(entity);
                return update == 0
                    ? NotFound((new { message = "Data Gagal Diubah Email dan Phone Tidak Boleh Sama Dengan Employee Lain" }))
                    : (ActionResult)Ok(new { message = "Data Berhasil Diubah" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
