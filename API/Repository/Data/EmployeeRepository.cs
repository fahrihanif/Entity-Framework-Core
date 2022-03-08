using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public override int Update(Employee entity, string key)
        {
            if (CheckEmailPhone(entity) == true)
            {
                return base.Update(entity, key);
            }
            return 0;
        }

        private bool CheckEmailPhone(Employee employee)
        {
            var nik = context.Employees.Where(s => !(s.NIK == employee.NIK)).ToList();
            var check = nik.Where(s => s.Email == employee.Email || s.Phone == employee.Phone).SingleOrDefault();

            if (check == null)
            {
                return true;
            }
            return false;
        }
    }
}
