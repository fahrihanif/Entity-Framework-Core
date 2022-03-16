﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
