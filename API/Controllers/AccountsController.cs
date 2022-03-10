using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //This class to implement BaseController in Account
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        //Get all return from method EmployeeRepostory ChangePassword and send result to postman
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM change)
        {
            try
            {
                var put = repository.ChangePassord(change);
                return put switch
                {
                    0 => NotFound(new { msg = "Akun Tidak Ditemukan!" }),
                    1 => NotFound(new { msg = "OTP Salah!" }),
                    2 => NotFound(new { msg = "OTP Sudah Pernah Digunakan!" }),
                    3 => NotFound(new { msg = "OTP Sudah Tidak Berlaku" }),
                    4 => NotFound(new { msg = "Password Tidak Sesuai" }),
                    _ => Ok(new { msg = "Password Berhasil Diubah" })
                };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
                
        }

        //Get all return from method EmployeeRepostory ForgotPassword and send result to postman
        [HttpPut("ForgotPassword")]
        [Route("")]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                var put = repository.ForgotPassword(email);
                return put == 0 
                    ? NotFound(new { msg = "Akun Tidak Ditemukan" }) 
                    : (ActionResult)Ok(new { msg = "OTP Berhasil Dikirim, Periksa Email Anda!" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Get all return from method EmployeeRepostory Login and send result to postman
        [HttpGet("Login")]
        public ActionResult Login(LoginVM login)
        {
            try
            {
                var get = repository.Login(login);
                return get switch
                {
                    0 => NotFound(new { msg = "Akun Tidak Ditemukan" }),
                    1 => NotFound(new { msg = "Password Salah" }),
                    _ => Ok(new { msg = "Login Berhasil" })
                };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Get all return from method EmployeeRepostory Register and send result to postman
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM register)
        {
            try
            {
                var post = repository.Register(register);
                return post == 0
                    ? NotFound(new { msg = "Data Gagal Disimpan Silahkan Periksa Kembali" })
                    : (ActionResult)Ok(new { msg = "Data Berhasil Disimpan" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
