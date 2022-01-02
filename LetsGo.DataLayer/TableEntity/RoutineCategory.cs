using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("RoutineCategory")]
    public class RoutineCategory : CommonEntity
    {
        [Column(Order = 0)]
        public Guid RoutineCategoryId { get; set; }
        [Column(Order = 1)]
        [Required]
        [StringLength(50)]
        public string RoutineCategoryName { get; set; }
        [Column(Order = 2)]
        [StringLength(50)]
        public string RoutineCategoryAltName { get; set; }

        public virtual ICollection<Routine> Routines { get; set; }
    }
}
