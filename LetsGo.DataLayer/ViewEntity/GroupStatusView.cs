using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class GroupStatusView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid GroupStatusId { get; set; }

        [Column(Order = 1)]
        public string GroupStatusName { get; set; }

        [Column(Order = 2)]
        public string GroupStatusAltName { get; set; }
    }
}
