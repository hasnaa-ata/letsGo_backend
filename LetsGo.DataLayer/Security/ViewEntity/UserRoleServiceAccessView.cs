using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.ViewEntity
{
    //[Table("UserRoleServiceAccessView", Schema = "security")]
    [NotMapped]
    public partial class UserRoleServiceAccessView : CommonBlock_Delete
    {
        [Column(Order = 0)]
        public Guid UserId { get; set; }
        
        [Column(Order = 1)]
        public Guid? RoleId { get; set; }

        [Column(Order = 2)]
        public Guid ServiceId { get; set; }
        
        [Column(Order = 3)]
        public string ServiceTag { get; set; }
        
        [Column(Order = 4)]
        public string ServiceName { get; set; }

        [Column(Order = 5)]
        public string ServiceAltName { get; set; }

        [Column(Order = 6)]
        public Guid ServiceAccessId { get; set; }

        [Column(Order = 7)]
        public Guid AccessTypeId { get; set; }

    }
}
