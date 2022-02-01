using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("GroupMedia")]
    public class GroupMedia : CommonEntity
    {
        [Column(Order = 0), Required]
        public Guid GroupMediaId { get; set; }

        [Column(Order = 1), Required]
        public Guid GroupId { get; set; }

        [Column(Order = 2), Required]
        [StringLength(10)]
        public string ContentType { get; set; }

        [Column(Order = 3), Required]
        [StringLength(200)]
        public string GroupMediaURL { get; set; }


        public virtual Group Group { get; set; }
    }
}
