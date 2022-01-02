using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("Country")]
    public class Country : CommonEntity
    {
        [Column(Order = 0)]
        public Guid CountryId { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(50)]

        public string CountryName { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        public string CountryAltName { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(5)]

        public string CountryCode { get; set; }

        [Column(Order = 4)]
        [StringLength(200)]
        public string PhoneRegExp { get; set; }

        [Column(Order = 5)]
        [StringLength(200)]
        public string MobileRegExp { get; set; }

        [Column(Order = 6)]
        [StringLength(200)]
        public string NationalIdentityRegExp { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
