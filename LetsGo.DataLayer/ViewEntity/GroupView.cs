using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class GroupView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid GroupId { get; set; }

        [Column(Order = 1)]
        public string GroupName { get; set; }

        [Column(Order = 2)]
        public string GroupAltName { get; set; }

        [Column(Order = 3)]
        public Guid RoutineId { get; set; }

        [Column(Order = 4)]
        public string Description { get; set; }

        [Column(Order = 5)]
        public string AltDescription { get; set; }

        [Column(Order = 6)]
        public Guid GroupStatusId { get; set; }

        [Column(Order = 7)]
        public string RoutineName { get; set; }

        [Column(Order = 8)]
        public string RoutineAltName { get; set; }

        [Column(Order = 9)]
        public string GroupStatusName { get; set; }

        [Column(Order = 10)]
        public string GroupStatusAltName { get; set; }

        [Column(Order = 11)]
        public int MaxNoMembers { get; set; }

        [Column(Order = 12)]
        public Guid UserId { get; set; }
    }
}
