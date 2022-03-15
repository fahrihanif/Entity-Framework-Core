using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Authorize]
    [Route("api/AccountRoles")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository repository;
        private readonly AccountRepository accountRepository;
        public AccountRolesController(AccountRoleRepository repository, AccountRepository accountRepository) : base(repository)
        {
            this.repository = repository;
            this.accountRepository = accountRepository;
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
                    : NotFound(new { message = "Role pada NIK ini sudah pernah ditambahkan" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Method use to sign in as manager
        [Authorize(Roles = "Director,Manager")]
        [HttpGet("SignManager")]
        public ActionResult SignManager(LoginVM login)
        {
            try
            {
                var get = accountRepository.Login(login);
                return get switch
                {
                    0 => NotFound(new { message = "Akun Tidak Ditemukan" }),
                    1 => NotFound(new { message = "Password Salah" }),
                    _ => Ok(new { message = "Login Sebagai Manager Berhasil" }),
                };
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
