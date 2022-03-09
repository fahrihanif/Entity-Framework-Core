using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

        public IEnumerable MasterEmployeeData()
        {
            var MasterEmployee = context.Employees
                .Join(context.Accounts,e => e.NIK,a => a.NIK,
                (e, a) => new { e, a })
                .Join(context.Profilings, ea => ea.a.NIK, p => p.NIK,
                (ea, p) => new { p, ea })
                .Join(context.Educations, eap => eap.p.EducationId, e => e.Id, 
                (eap, e) => new { e, eap })
                .Join( context.Universities, eape => eape.e.UniversityId,u => u.Id,
                (eape, u) => new {
                    NIK = eape.eap.ea.e.NIK,
                    FullName = (eape.eap.ea.e.FirstName+" "+ eape.eap.ea.e.LastName),
                    Phone = eape.eap.ea.e.Phone,
                    Gender = eape.eap.ea.e.Gender,
                    Email = eape.eap.ea.e.Email,
                    BithDate = eape.eap.ea.e.BirthDate,
                    Salary = eape.eap.ea.e.Salary,
                    Education_Id = eape.eap.p.EducationId,
                    GPA = eape.e.GPA,
                    Degree = eape.e.Degree,
                    UniversityName = u.Name
                });

            return MasterEmployee;
        }

        public override int Update(Employee entity)
        {
            return CheckEmailPhone(entity) 
                ? base.Update(entity) 
                : 0;
        }

        private bool CheckEmailPhone(Employee employee)
        {
            var nik = context.Employees.Where(s => !(s.NIK == employee.NIK)).ToList();
            var check = nik.Where(s => s.Email == employee.Email || s.Phone == employee.Phone).SingleOrDefault();

            return check == null;
        }
    }
}
