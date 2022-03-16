using API.Context;
using API.Models;

namespace API.Repository.Data
{
    //This class to implement GeneralRepository in Role
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
