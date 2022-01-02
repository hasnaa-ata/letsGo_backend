using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.TableEntity
{
    [Table("UserService", Schema = "security")]
    public partial class UserService : CommonEntity
    {
        [Column(Order = 0)]
        public Guid UserServiceId { get; set; }

        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public Guid ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
