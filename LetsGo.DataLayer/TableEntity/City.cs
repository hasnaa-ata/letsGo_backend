using DataLayer.Common;
using DataLayer.Security.TableEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("City")]
    public class City : CommonEntity
    {
        [Column(Order = 0)]
        public Guid CityId { get; set; }

        [Column(Order = 1)]
        public Guid StateId { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string CityAltName { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
