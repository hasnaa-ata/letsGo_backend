using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Security.TableEntity
{
    [Table("AccessType", Schema = "security")]
    public class AccessType
    {
        [Column(Order = 0)]
        public Guid AccessTypeId { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(50)]
        public string AccessTypeName { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        public string AccessTypeAltName { get; set; }

        public virtual ICollection<ServiceAccess> ServiceAccess { get; set; }
    }
}
