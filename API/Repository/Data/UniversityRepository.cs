using API.Context;
using API.Models;

namespace API.Repository.Data
{
    //This class to implement GeneralRepository in University
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
