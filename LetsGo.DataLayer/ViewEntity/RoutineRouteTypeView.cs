using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class RoutineRouteTypeView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoutineRouteTypeId { get; set; }

        [Column(Order = 1)]
        public string RoutineRouteTypeName { get; set; }

        [Column(Order = 2)]
        public string RoutineRouteTypeAltName { get; set; }
    }
}
