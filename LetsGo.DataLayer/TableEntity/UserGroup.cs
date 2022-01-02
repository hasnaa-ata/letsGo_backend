using DataLayer.Common;
using DataLayer.Security.TableEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("UserGroup")]
    public class UserGroup : CommonEntity
    {
        [Column(Order = 0)]
        public Guid UserGroupId { get; set; }

        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public Guid GroupId { get; set; }

        [Column(Order = 3)]
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual Group Group { get; set; }
    }
}
