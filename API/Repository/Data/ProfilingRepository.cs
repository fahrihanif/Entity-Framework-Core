using API.Context;
using API.Models;

namespace API.Repository.Data
{
    //This class to implement GeneralRepository in Profiling
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
