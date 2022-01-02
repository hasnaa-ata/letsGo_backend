using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class UserGroupView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserGroupId { get; set; }

        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public Guid GroupId { get; set; }

        [Column(Order = 3)]
        public Guid RoleId { get; set; }

        [Column(Order = 4)]
        public string UserFullName { get; set; }

        [Column(Order = 5)]
        public string UserAltFullName { get; set; }

        [Column(Order = 6)]
        public string RoleName { get; set; }

        [Column(Order = 7)]
        public string RoleAltName { get; set; }

        [Column(Order = 8)]
        public string GroupName { get; set; }

        [Column(Order = 9)]
        public string GroupAltName { get; set; }

    }
}
