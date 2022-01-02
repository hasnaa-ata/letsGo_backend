using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("RoutineDay")]
    public class RoutineDay : CommonEntity
    {
        [Column(Order = 0)]
        public Guid RoutineDayId { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(10)]
        public string Day { get; set; }

        [Column(Order = 2)]
        public TimeSpan FromTime { get; set; }

        [Column(Order = 3)]
        public TimeSpan ToTime { get; set; }

        [Column(Order = 4)]
        public Guid RoutineId { get; set; }


        public Routine Routine { get; set; }
    }
}
