using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    //This class to implement BaseController in Employee
    //[Authorize]
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
        [HttpGet("UniversityCount")]
        public ActionResult GetUniversityCount()
        {
            try
            {
                var get = repository.TotalUniversity();
                return get == null
                    ? NotFound(new { message = "Data Tidak Ada" })
                    : (ActionResult)Ok(repository.TotalUniversity());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [HttpGet("GenderCount")]
        public ActionResult GetGenderCount()
        {
            try
            {
                var get = repository.TotalGender();
                return get == null
                    ? NotFound(new { message = "Data Tidak Ada" })
                    : (ActionResult)Ok(repository.TotalGender());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Get Data master
        //[Authorize(Roles = "Director, Manager")]
        [AllowAnonymous]
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
        //[Authorize]
        [AllowAnonymous]
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
