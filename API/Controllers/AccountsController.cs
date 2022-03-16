using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    //This class to implement BaseController in Account
    [Authorize]
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;

        private IConfiguration configuration;
        public AccountsController(AccountRepository repository, IConfiguration configuration) : base(repository)
        {
            this.repository = repository;
            this.configuration = configuration;
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
                    0 => NotFound(new { message = "Akun Tidak Ditemukan!" }),
                    1 => NotFound(new { message = "OTP Salah!" }),
                    2 => NotFound(new { message = "OTP Sudah Pernah Digunakan!" }),
                    3 => NotFound(new { message = "OTP Sudah Tidak Berlaku" }),
                    4 => NotFound(new { message = "Password Tidak Sesuai" }),
                    _ => Ok(new { message = "Password Berhasil Diubah" })
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
                    ? NotFound(new { message = "Akun Tidak Ditemukan" })
                    : (ActionResult)Ok(new { message = "OTP Berhasil Dikirim, Periksa Email Anda!" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Get all return from method EmployeeRepostory Login and send result to postman
        [AllowAnonymous]
        [HttpGet("Login")]
        public ActionResult Login(LoginVM login)
        {
            try
            {
                var get = repository.Login(login);
                switch (get)
                {
                    case 0:
                        return NotFound(new { message = "Akun Tidak Ditemukan" });
                    case 1:
                        return NotFound(new { message = "Password Salah" });
                    default:
                        var role = repository.UserRole(login.Email);

                        var claims = new List<Claim>()
                        {
                            new Claim("email", login.Email)
                        };

                        foreach (var i in role)
                        {
                            claims.Add(new Claim("role", i));
                        }

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            configuration["Jwt:Issuer"],
                            configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                            );

                        var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                        claims.Add(new Claim("Token Security", idToken.ToString()));

                        return Ok(new { status = HttpStatusCode.OK, idToken, message = "Login Berhasil" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Get all return from method EmployeeRepostory Register and send result to postman
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM register)
        {
            try
            {
                var post = repository.Register(register);
                return post == 0
                    ? NotFound(new { message = "Data Gagal Disimpan Silahkan Periksa Kembali" })
                    : (ActionResult)Ok(new { message = "Data Berhasil Disimpan" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
