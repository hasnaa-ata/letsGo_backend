using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.TableEntity
{
    [Table("UserServiceAccess", Schema = "security")]
    public partial class UserServiceAccess : CommonEntity
    {
        [Column(Order = 0)]
        public Guid UserServiceAccessId { get; set; }

        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public Guid ServiceAccessId { get; set; }

        public virtual ServiceAccess ServiceAccess { get; set; }
        public virtual User User { get; set; }
    }
}
