using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/AccountRoles")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository repository;
        private readonly AccountRepository accountRepository;
        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        //Insert data to entity
        [Authorize(Roles = "Director")]
        [HttpPost]
        public override ActionResult Post(AccountRole role)
        {
            try
            {
                var post = repository.CheckRole(role);
                return post 
                    ? base.Post(role) 
                    : NotFound(new { msg = "Role pada NIK ini sudah pernah ditambahkan" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpGet("SignManager")]
        public ActionResult SignManager(LoginVM login)
        {
            try
            {
                var get = accountRepository.Login(login);
                return get switch
                {
                    0 => NotFound(new { msg = "Akun Tidak Ditemukan" }),
                    1 => NotFound(new { msg = "Password Salah" }),
                    _ => Ok(new { msg = "Login Sebagai Manager Berhasil" }),
                };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
    }
}
