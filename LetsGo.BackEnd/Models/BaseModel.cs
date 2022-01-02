using GenericBackEndCore.Classes.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class BaseModel<TDBEntity, TDBViewEntity, TEntityDAL> : GenericModel<TDBEntity, TDBViewEntity, TEntityDAL>
    {
        public BaseModel() : base()
        {

        }
        public BaseModel(DbContext context) : base(context)
        {

        }
    }
}
