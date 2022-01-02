using DataLayer.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mawid.DataLayer.ViewEntity
{
    [NotMapped]
    public class CityView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid CityId { get; set; }

        [Column(Order = 1)]
        public string CityName { get; set; }

        [Column(Order = 2)]
        public string CityAltName { get; set; }

        [Column(Order = 3)]
        public Guid StateId { get; set; }

        [Column(Order = 4)]
        public string StateName { get; set; }

        [Column(Order = 5)]
        public string StateAltName { get; set; }

        [Column(Order = 6)]
        public Guid CountryId { get; set; }

        [Column(Order = 7)]
        public string CountryName { get; set; }

        [Column(Order = 8)]
        public string CountryAltName { get; set; }

        [Column(Order = 6)]
        public string CountryCode { get; set; }
    }
}
