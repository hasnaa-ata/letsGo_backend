using DataLayer.Security.Common;
using LetsGo.DataLayer.TableEntity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Security.TableEntity
{
    [Table("User", Schema = "security")]
    public class User : IdentityUser<Guid>, ICommonSecurityEntity
    {
        [Column(Order = 2)]
        [StringLength(256)]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [Column(Order = 3)]
        [Required]
        [StringLength(150)]
        public string UserFullName { get; set; }

        [Column(Order = 4)]
        [StringLength(150)]
        public string UserAltFullName { get; set; }

        [Column(Order = 7)]
        [StringLength(20)]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Column(Order = 7)]
        [StringLength(200)]
        public string ImageURL { get; set; }

        [Column(Order = 10)]
        public Guid? CityId { get; set; }

        [Column(Order = 11)]
        [StringLength(200)]
        public string JobTitle { get; set; }

        [Column(Order = 12)]
        [StringLength(200)]
        public string WorkPlace { get; set; }

        [Column(Order = 13)]
        [StringLength(10)]
        public string ImageContentType { get; set; }

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

        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<FirebaseToken> FirebaseTokens { get; set; }
        public virtual ICollection<UserService> UserServices { get; set; }
        public virtual ICollection<UserServiceAccess> UserServiceAccesses { get; set; }
        public virtual ICollection<Routine> Routines { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
