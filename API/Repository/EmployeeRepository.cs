using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private Employee emp = new Employee();
        private readonly MyContext context;

        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(string NIK)
        {
            var check = context.Employees.Find(NIK);
            if (check != null)
            {
                context.Remove(context.Employees.Find(NIK));
                var result = context.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }

        public int Insert(Employee employee)
        {
            if (context.Employees.Count() == 0)
            {
                employee.NIK = DateTime.Now.ToString("yyyy") + "000";
            }
            else
            {
                emp = context.Employees.OrderBy(e => e.NIK).Last();
                employee.NIK = (Convert.ToInt32(emp.NIK) + 1).ToString();
            }

            if (Duplicate(employee) == true)
            {
                context.Employees.Add(employee);
                var result = context.SaveChanges();
                return result;
            }
            return 0;
        }

        public int Update(Employee employee)
        {
            if (DuplicateUp(employee) == true)
            {
                context.Entry(employee).State = EntityState.Modified;
                var result = context.SaveChanges();
                return result;
            }
            return 0;
        }

        private bool Duplicate(Employee employee)
        {
            var duplicate = context.Employees.Where(s => s.Email == employee.Email || s.Phone == employee.Phone).SingleOrDefault();
            
            if (duplicate == null)
            {
                return true;
            }
            return false;
        }

        private bool DuplicateUp(Employee employee)
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
