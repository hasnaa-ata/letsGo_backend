using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.TableEntity
{
    [Table("Service", Schema = "security")]
    public class Service : CommonBlock
    {
        [Column(Order = 0)]
        public Guid ServiceId { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(50)]
        public string ServiceTag { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }

        [Column(Order = 3)]
        [StringLength(100)]
        public string ServiceAltName { get; set; }

        [Column(Order = 4)]
        [StringLength(200)]
        public string ServiceDescription { get; set; }

        [Column(Order = 5)]
        [StringLength(200)]
        public string ServiceAltDescription { get; set; }

        public virtual ICollection<RoleService> RoleServices { get; set; }
        public virtual ICollection<ServiceAccess> ServiceAccesses { get; set; }
        public virtual ICollection<UserService> UserServices { get; set; }
    }
}
