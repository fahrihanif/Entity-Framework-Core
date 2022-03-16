using API.Context;
using API.Models;

namespace API.Repository.Data
{
    //This class to implement GeneralRepository in Education
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
