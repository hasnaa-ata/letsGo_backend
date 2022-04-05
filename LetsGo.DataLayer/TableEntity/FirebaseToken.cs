using DataLayer.Common;
using DataLayer.Security.TableEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LetsGo.DataLayer.TableEntity
{
    [Table("FirebaseToken")]
    public class FirebaseToken : CommonEntity
    {
        [Column(Order = 0)]
        public Guid FirebaseTokenId { get; set; }

        [Column(Order = 1), Required]
        [StringLength(500)]
        public string Token { get; set; }

        [Column(Order = 2)]
        [StringLength(20)]
        public string Platform { get; set; }

        [Column(Order = 3)]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
