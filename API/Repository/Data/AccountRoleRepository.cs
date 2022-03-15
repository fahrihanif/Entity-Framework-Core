using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext context;
        public AccountRoleRepository(MyContext context) : base(context)
        {
            this.context = context;
        }

        //This method is to check whether it has been added before or not
        public bool CheckRole(AccountRole role)
        {
            return context.AccountRoles.SingleOrDefault(a => a.AccountNIK == role.AccountNIK && a.RoleId == role.RoleId) == null;
        }
    }
}
