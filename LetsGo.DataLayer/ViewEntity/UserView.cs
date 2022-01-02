using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.ViewEntity
{
    [NotMapped]
    public class UserView : CommonViewEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserId { get; set; }

        [Column(Order = 1)]
        public string UserName { get; set; }

        [Column(Order = 2)]
        public string UserFullName { get; set; }

        [Column(Order = 3)]
        public string UserAltFullName { get; set; }

        [Column(Order = 4)]
        public string ImageURL { get; set; }

        [Column(Order = 6)]
        public string Email { get; set; }

        [Column(Order = 7)]
        public string PhoneNumber { get; set; }

        [Column(Order = 8)]
        public Guid? CityId { get; set; }

        [Column(Order = 9)]
        public string CityName { get; set; }

        [Column(Order = 10)]
        public string CityAltName { get; set; }

        [Column(Order = 11)]
        public string JobTitle { get; set; }

        [Column(Order = 12)]
        public string WorkPlace { get; set; }
    }
}
