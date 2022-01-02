﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Security.Common
{
    public interface ICommonSecurityEntity
    {
        [Column(Order = 500)]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsBlock { get; set; }

        [Column(Order = 501)]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Column(Order = 502)]
        public Guid CreateUserId { get; set; }

        [Column(Order = 503, TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [Column(Order = 504)]
        public Guid? ModifyUserId { get; set; }

        [Column(Order = 505, TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }
    }

}
