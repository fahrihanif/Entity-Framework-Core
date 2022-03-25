using API.Context;
using API.Models;
using API.ViewModel;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    //This class to implement GeneralRepository in Employee
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public IEnumerable TotalUniversity()
        {
            return context.Universities
                .Select(u => new
                {
                    Labels = u.Name,
                    Series = u.Educations.Count
                });
        }

        public ChartVM TotalGender()
        {
            string[] labels = { Gender.Male.ToString(), Gender.Female.ToString() };
            int[] series = { context.Employees.Where(e => e.Gender == Gender.Male).Count(),
                            context.Employees.Where(e => e.Gender == Gender.Female).Count()};

            return new ChartVM(labels, series);
        }

        //This method to get all Entities
        public IEnumerable MasterEmployeeData()
        {

            return context.Employees
                .Join(context.Accounts, e => e.NIK, a => a.NIK,
                (e, a) => new { e, a })
                .Join(context.Profilings, ea => ea.a.NIK, p => p.NIK,
                (ea, p) => new { p, ea })
                .Join(context.Educations, eap => eap.p.EducationId, e => e.Id,
                (eap, e) => new { e, eap })
                .Join(context.Universities, eape => eape.e.UniversityId, u => u.Id,
                (eape, u) => new
                {
                    NIK = eape.eap.ea.e.NIK,
                    FullName = (eape.eap.ea.e.FirstName + " " + eape.eap.ea.e.LastName),
                    Phone = eape.eap.ea.e.Phone,
                    Gender = eape.eap.ea.e.Gender.ToString(),
                    Email = eape.eap.ea.e.Email,
                    BirthDate = eape.eap.ea.e.BirthDate,
                    Salary = eape.eap.ea.e.Salary,
                    Education_Id = eape.eap.p.EducationId,
                    GPA = eape.e.GPA,
                    Degree = eape.e.Degree,
                    UniversityName = u.Name
                });
        }

        //override method update in GeneralRepository to check entity before update
        public override int Update(Employee entity)
        {
            return CheckEmailPhone(entity)
                ? base.Update(entity)
                : 0;
        }

        //This method used to check are email and phone is have been used or not
        private bool CheckEmailPhone(Employee employee)
        {
            var nik = context.Employees.Where(s => !(s.NIK == employee.NIK)).ToList();
            var check = nik.Where(s => s.Email == employee.Email || s.Phone == employee.Phone).SingleOrDefault();

            return check == null;
        }
    }
}
