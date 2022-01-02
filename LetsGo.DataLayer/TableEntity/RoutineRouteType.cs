using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    public class RoutineRouteType : CommonEntity
    {
        [Column(Order = 0)]
        public Guid RoutineRouteTypeId { get; set; }
        [Column(Order = 1)]
        [Required]
        [StringLength(50)]
        public string RoutineRouteTypeName { get; set; }
        [Column(Order = 2)]
        [StringLength(50)]
        public string RoutineRouteTypeAltName { get; set; }

        public virtual ICollection<Routine> Routines { get; set; }
    }
}
