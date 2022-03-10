using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //This class to implement BaseController in Employee
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        //Get Data
        [HttpGet("Master")]
        public ActionResult GetAllMaster()
        {
            try
            {
                var get = repository.MasterEmployeeData();
                return get == null
                    ? NotFound(new { msg = "Data Tidak Ada" })
                    : (ActionResult)Ok(repository.MasterEmployeeData());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Update
        [HttpPut]
        public override ActionResult Update(Employee entity)
        {
            try
            {
                var update = repository.Update(entity);
                return update == 0
                    ? NotFound((new { msg = "Data Gagal Diubah Email dan Phone Tidak Boleh Sama Dengan Employee Lain" }))
                    : (ActionResult)Ok(new { msg = "Data Berhasil Diubah" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
