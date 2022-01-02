using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Common;

namespace DataLayer.Security.ViewEntity
{
    //[Table("ServiceAccessView", Schema = "security")]
    [NotMapped]
    public partial class ServiceAccessView : CommonBlock
    {
        [Key]
        [Column(Order = 0)]
        public Guid ServiceAccessId { get; set; }
        
        [Column(Order = 1)]
        public Guid ServiceId { get; set; }
        
        [Column(Order = 2)]
        public string ServiceTag { get; set; }
        
        [Column(Order = 3)]
        public string ServiceName { get; set; }

        [Column(Order = 4)]
        public string ServiceAltName { get; set; }
        
        [Column(Order = 5)]
        public Guid AccessTypeId { get; set; }
        
        [Column(Order = 6)]
        public string AccessTypeName { get; set; }

        [Column(Order = 7)]
        public string AccessTypeAltName { get; set; }


    }
}
