using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class RoutineCategoryView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoutineCategoryId { get; set; }

        [Column(Order = 1)]
        public string RoutineCategoryName { get; set; }

        [Column(Order = 2)]
        public string RoutineCategoryAltName { get; set; }
    }
}
