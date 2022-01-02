using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.ViewEntity
{
    //[Table("UserRoleView", Schema = "security")]
    [NotMapped]
    public partial class UserRoleView
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserRoleId { get; set; }
        
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public Guid RoleId { get; set; }

        [Column(Order = 3)]
        public string RoleName { get; set; }

        [Column(Order = 4)]
        public string RoleAltName { get; set; }


        public string IsBlock_str { get; set; }
        public string CreateUser_FullName { get; set; }
        public string CreateUser_FullAltName { get; set; }

    }
}
