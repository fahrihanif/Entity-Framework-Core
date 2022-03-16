using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //This class to implement BaseController in Univerisity
    [Authorize]
    [Route("api/Universities")]
    [ApiController]
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
        }
    }
}
