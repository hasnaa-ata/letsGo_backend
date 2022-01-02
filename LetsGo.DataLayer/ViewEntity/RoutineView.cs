using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class RoutineView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoutineId { get; set; }

        [Column(Order = 1)]
        public string RoutineName { get; set; }

        [Column(Order = 2)]
        public string RoutineAltName { get; set; }

        [Column(Order = 3)]
        public Guid? RoutineCategoryId { get; set; }

        [Column(Order = 4)]
        public Guid RoutineRouteTypeId { get; set; }

        [Column(Order = 5)]
        public string Description { get; set; }

        [Column(Order = 6)]
        public string AltDescription { get; set; }

        [Column(Order = 7)]
        public double RoutineSourceLatitude { get; set; }

        [Column(Order = 8)]
        public double RoutineSourceLongtitude { get; set; }

        [Column(Order = 9)]
        public double RoutineDestinationLatitude { get; set; }

        [Column(Order = 10)]
        public double RoutineDestinationLongtitude { get; set; }

        [Column(Order = 11)]
        public string RoutineSourceAdderss { get; set; }

        [Column(Order = 12)]
        public string RoutineDestinationAdderss { get; set; }

        [Column(Order = 13)]
        public string RoutineCategoryName { get; set; }

        [Column(Order = 14)]
        public string RoutineCategoryAltName { get; set; }

        [Column(Order = 15)]
        public string RoutineRouteTypeName { get; set; }

        [Column(Order = 16)]
        public string RoutineRouteTypeAltName { get; set; }

        [Column(Order = 17)]
        public Guid UserId { get; set; }
    }
}
