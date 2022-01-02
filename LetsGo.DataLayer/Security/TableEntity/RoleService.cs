using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.TableEntity
{
    [Table("RoleService", Schema = "security")]
    public class RoleService : CommonEntity
    {
        [Column(Order = 0)]
        public Guid RoleServiceId { get; set; }

        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [Column(Order = 2)]
        public Guid ServiceId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Service Service { get; set; }
    }
}
