using DataLayer.Common;
using DataLayer.Security.TableEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("Routine")]
    public class Routine : CommonEntity
    {
        [Column(Order = 0)]
        public Guid RoutineId { get; set; }

        [Column(Order = 1), Required, StringLength(250)]
        public string RoutineName { get; set; }

        [Column(Order = 2), StringLength(250)]
        public string RoutineAltName { get; set; }

        [Column(Order = 3)]
        public Guid? RoutineCategoryId { get; set; }

        [Column(Order = 4)]
        public Guid RoutineRouteTypeId { get; set; }

        [Column(Order = 5), StringLength(300)]
        public string Description { get; set; }

        [Column(Order = 6), StringLength(300)]
        public string AltDescription { get; set; }

        [Column(Order = 7)]
        public double RoutineSourceLatitude { get; set; }

        [Column(Order = 8)]
        public double RoutineSourceLongtitude { get; set; }

        [Column(Order = 9)]
        public double RoutineDestinationLatitude { get; set; }

        [Column(Order = 10)]
        public double RoutineDestinationLongtitude { get; set; }

        [Column(Order = 11), StringLength(300)]
        public string RoutineSourceAdderss { get; set; }

        [Column(Order = 12), StringLength(300)]
        public string RoutineDestinationAdderss { get; set; }

        [Column(Order = 13)]
        public Guid UserId { get; set; }

        public virtual RoutineCategory RoutineCategory { get; set; }
        public virtual RoutineRouteType RoutineRouteType { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<RoutineDay> RoutineDays { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
