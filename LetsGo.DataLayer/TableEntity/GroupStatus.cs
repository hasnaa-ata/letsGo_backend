using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("GroupStatus")]
    public class GroupStatus : CommonEntity
    {
        [Column(Order = 0)]
        public Guid GroupStatusId { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(50)]
        public string GroupStatusName { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        public string GroupStatusAltName { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
