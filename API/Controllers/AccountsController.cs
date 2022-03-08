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
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        //private readonly IEmailService emailService;
        public AccountsController(AccountRepository repository/*, IEmailService emailService*/) : base(repository)
        {
            //this.emailService = emailService;
            this.repository = repository;
        }

        [HttpPut("ChangePassword")]
        [Route("")]
        public ActionResult ChangePassword([FromHeader]ChangePasswordVM change)
        {
            var put = repository.ChangePassord(change);

            return put switch
            {
                0 => NotFound(new { msg = "OTP Salah!" }),
                1 => NotFound(new { msg = "OTP Sudah Pernah Digunakan!" }),
                2 => NotFound(new { msg = "OTP Sudah Tidak Berlaku" }),
                3 => NotFound(new { msg = "Password Tidak Sesuai" }),
                _ => Ok(new { msg = "Password Berhasil Diubah" })
            };
                
        }

        [HttpPut("ForgotPassword")]
        [Route("")]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                var put = repository.ForgotPassword(email);
                if (put == 0)
                {
                    return NotFound(new { msg = "Akun Tidak Ditemukan" });
                }
                return Ok(new { msg = "OTP Berhasil Dikirim, Periksa Email Anda!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e });
            }
        }

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
                return BadRequest(new { msg = e });
            }
        }

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
                return BadRequest(new { msg = e });
            }
        }
    }
}
