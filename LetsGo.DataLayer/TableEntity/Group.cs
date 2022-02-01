using DataLayer.Common;
using DataLayer.Security.TableEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("Group")]
    public class Group : CommonEntity
    {
        [Column(Order = 0)]
        public Guid GroupId { get; set; }

        [Column(Order = 1), Required, StringLength(250)]
        public string GroupName { get; set; }

        [Column(Order = 2), StringLength(250)]
        public string GroupAltName { get; set; }

        [Column(Order = 3)]
        public Guid RoutineId { get; set; }

        [Column(Order = 4), StringLength(350)]
        public string Description { get; set; }

        [Column(Order = 5), StringLength(350)]
        public string AltDescription { get; set; }

        [Column(Order = 6)]
        public Guid GroupStatusId { get; set; }

        [Column(Order = 7)]
        public int MaxNoMembers { get; set; }

        [Column(Order = 8)]
        [StringLength(10)]
        public string ImageContentType { get; set; }

        [Column(Order = 9)]
        [StringLength(200)]
        public string GroupImageURL { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<GroupMedia> GroupMedias { get; set; }

        public virtual Routine Routine { get; set; }
        public virtual GroupStatus GroupStatus { get; set; }
    }
}
