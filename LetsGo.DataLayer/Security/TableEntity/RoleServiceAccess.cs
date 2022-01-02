using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.TableEntity
{
    [Table("RoleServiceAccess", Schema = "security")]
    public class RoleServiceAccess : CommonEntity
    {
        [Column(Order = 0)]
        public Guid RoleServiceAccessId { get; set; }

        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [Column(Order = 2)]
        public Guid ServiceAccessId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ServiceAccess ServiceAccess { get; set; }
    }
}
