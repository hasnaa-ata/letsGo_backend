using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Security.Common;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Security.TableEntity
{
    [Table("Role", Schema = "security")]
    public class Role : IdentityRole<Guid>, ICommonSecurityEntity
    {

        [Column(Order = 1)]
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        public string RoleAltName { get; set; }

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

        [Column(Order = 504)]
        public Guid? ModifyUserId { get; set; }

        [Column(Order = 505, TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<RoleService> RoleServices { get; set; }
        public virtual ICollection<RoleServiceAccess> RoleServiceAccesses { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
