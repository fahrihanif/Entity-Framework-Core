using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_account_role")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Account")]
        public string AccountNIK { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        //Relation
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
