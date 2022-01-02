using DataLayer.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Security.ViewEntity
{
    //[Table("RoleServiceAccessView", Schema = "security")]
    [NotMapped]
    public class RoleServiceAccessView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoleServiceAccessId { get; set; }

        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [Column(Order = 2)]
        public Guid ServiceAccessId { get; set; }

        [Column(Order = 3)]
        public Guid ServiceId { get; set; }

        [Column(Order = 4)]
        public Guid AccessTypeId { get; set; }
    }
}
