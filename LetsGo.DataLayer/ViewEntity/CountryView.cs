using DataLayer.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mawid.DataLayer.ViewEntity
{
    [NotMapped]
    public class CountryView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid CountryId { get; set; }

        [Column(Order = 1)]
        public string CountryName { get; set; }

        [Column(Order = 2)]
        public string CountryAltName { get; set; }

        [Column(Order = 3)]
        public string PhoneRegExp { get; set; }

        [Column(Order = 4)]
        public string MobileRegExp { get; set; }

        [Column(Order = 5)]
        public string NationalIdentityRegExp { get; set; }

        [Column(Order = 6)]
        public string CountryCode { get; set; }
    }
}
