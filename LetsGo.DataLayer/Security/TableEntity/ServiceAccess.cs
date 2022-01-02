using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;
using DataLayer.Security.Common;

namespace DataLayer.Security.TableEntity
{
    [Table("ServiceAccess", Schema = "security")]
    public partial class ServiceAccess : CommonBlock
    {
        [Column(Order = 0)]
        public Guid ServiceAccessId { get; set; }

        [Column(Order = 1)]
        public Guid ServiceId { get; set; }

        [Column(Order = 2)]
        public Guid AccessTypeId { get; set; }

        public virtual AccessType AccessType { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<RoleServiceAccess> RoleServiceAccesses { get; set; }
        public virtual ICollection<UserServiceAccess> UserServiceAccesses { get; set; }
    }
}
