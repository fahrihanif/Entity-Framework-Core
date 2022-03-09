using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
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

        public int ChangePassord(ChangePasswordVM change)
        {
            var account = context.Employees.Where(e => e.Email == change.Email)
                .Join(context.Accounts, a => a.NIK, e => e.NIK,
                (e, a) => new
                {
                    NIK = a.NIK,
                    Email = e.Email,
                    Password = a.Password,
                    OTP = a.OTP,
                    ExpiredToken = a.ExpiredToken,
                    IsUsed = a.IsUsed
                }).SingleOrDefault();

            if (account != null)
            {
                if (change.OTP == account.OTP)
                {
                    if (account.IsUsed == false)
                    {
                        if (account.ExpiredToken > DateTime.Now)
                        {
                            if (change.NewPassword == change.ConfirmPassword)
                            {
                                base.Update(new Account
                                {
                                    NIK = account.NIK,
                                    Password = change.NewPassword,
                                    OTP = account.OTP,
                                    ExpiredToken = account.ExpiredToken,
                                    IsUsed = true
                                });
                                return 5;
                            }
                            return 4;
                        }
                        return 3;
                    }
                    return 2;
                }
                return 1;
            }
            return 0;
        }

        public int ForgotPassword(String email)
        {
            var account = context.Employees.Join
                (context.Accounts, a => a.NIK, e => e.NIK, 
                (e, a) => new {
                    NIK = a.NIK,
                    Email = e.Email,
                    Password = a.Password
            }).SingleOrDefault(e => e.Email == email);

            if (account != null)
            {
                Account acc = new Account
                {
                    NIK = account.NIK,
                    Password = account.Password,
                    OTP = new Random().Next(100000, 999999),
                    ExpiredToken = DateTime.Now.AddMinutes(5),
                    IsUsed = false
                };
                Update(acc);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Forgot Password EF Core","kaibee3333@gmail.com"));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = $"Change Password OTP ({acc.OTP})";
                message.Body = new TextPart("Plain") { Text = $"Kode OTP : {acc.OTP}"};

                SmtpClient smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 465, true);
                smtp.Authenticate("kaibee3333@gmail.com", "Kaibe333");
                smtp.Send(message);
                smtp.Disconnect(true);

                return 1;
            }
            return 0;
        }

        public int Login(LoginVM login)
        {
            var check = context.Employees.Join(context.Accounts, a => a.NIK, e => e.NIK, (e, a) => new
            {
                NIK = a.NIK,
                Email = e.Email,
                Password = a.Password
            }).SingleOrDefault(e => e.Email == login.Email);

            if (check != null)
            {
                return check.Password == login.Password
                    ? 2
                    : 1;
            }
            return 0;
        }

        public int Register(RegisterVM register)
        {
            var empCount = context.Employees.Count() + 1;
            var Year = DateTime.Now.Year;
            register.NIK = Year + "00" + empCount.ToString();

            if (CheckEmailPhone(register))
            {
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
                context.Employees.Add(emp);

                context.Accounts.Add(new Account
                {
                    NIK = emp.NIK,
                    Password = register.Password
                });

                Education edu = new Education
                {
                    GPA = register.GPA,
                    Degree = register.Degree,
                    UniversityId = register.UniversityId
                };
                context.Educations.Add(edu);
                context.SaveChanges();

                context.Profilings.Add(new Profiling
                {
                    NIK = emp.NIK,
                    EducationId = edu.Id
                });
                context.SaveChanges();

                return 1;
            }
            return 0;
        }

        private bool CheckEmailPhone(RegisterVM register)
        {
            var check = context.Employees.Where(s => s.Email == register.Email || s.Phone == register.Phone).SingleOrDefault();

            return check == null;
        }
    }
}
