using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
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
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
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
