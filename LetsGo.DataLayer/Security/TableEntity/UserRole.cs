using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Security.Common;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Security.TableEntity
{
    [Table("UserRole", Schema = "security")]
    public partial class UserRole : IdentityUserRole<Guid>
    {

        [Column(Order = 500)]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsBlock { get; set; }

        [Column(Order = 501)]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Column(Order = 502)]
        public Guid CreateUserId { get; set; }

        [Column(Order = 503, TypeName = "datetime")]
        public DateTime CreateDate { get; set; }


        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
