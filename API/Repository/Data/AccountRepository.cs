using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public int Register(RegisterVM register)
        {
            var empCount = context.Employees.Last().NIK == null ? 1 : Convert.ToInt32(context.Employees.Last().NIK) + 1;
            //var eduCheck = context.Educations.Where(e => e.GPA == register.GPA && e.Degree == register.Degree && e.UniversityId == register.UniversityId).SingleOrDefault();
            var Year = DateTime.Now.Year;
            register.NIK = Year + "00" + empCount.ToString();

            Employee emp = new Employee
            {
                NIK = register.NIK,
                FirstName = register.FirstName,
                LastName = register.LastName,
                BirthDate = register.BirthDate,
                Gender = register.Gender,
                Phone = register.Phone,
                Salary = register.Salary,
                Email = register.Email
            };

            Account acc = new Account
            {
                NIK = emp.NIK,
                Password = register.Password
            };

            Education edu = new Education
            {
                GPA = register.GPA,
                Degree = register.Degree,
                UniversityId = register.UniversityId
            };

            Profiling pro = new Profiling {
                NIK = emp.NIK,
                Education = edu
            };

            Console.WriteLine(register.BirthDate);

            if (CheckEmailPhone(register) == true)
            {
                context.Employees.Add(emp);
                context.Educations.Add(edu);
                context.Accounts.Add(acc);
                context.Profilings.Add(pro);
                var result = context.SaveChanges();
                return result;
            }
            return 0;
        }

        private bool CheckEmailPhone(RegisterVM register)
        {
            var check = context.Employees.Where(s => s.Email == register.Email || s.Phone == register.Phone).SingleOrDefault();

            if (check == null)
            {
                return true;
            }
            return false;
        }
    }
}
