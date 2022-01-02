using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("State")]
    public class State : CommonEntity
    {
        [Column(Order = 0)]
        public Guid StateId { get; set; }

        [Column(Order = 1)]
        public Guid CountryId { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(50)]
        public string StateName { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string StateAltName { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
