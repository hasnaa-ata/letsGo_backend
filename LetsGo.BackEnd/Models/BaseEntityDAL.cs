using GenericBackEndCore.Classes.Common;
using LetsGo.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class BaseEntityDAL<TDBEntity, TDBViewEntity, TViewEntityDAL> : GenericEntityDAL<TDBEntity, TDBViewEntity, TViewEntityDAL>
    where TDBEntity : class, new()
    where TDBViewEntity : class, new()
    where TViewEntityDAL : class, new()
    {
        public BaseEntityDAL() : base(new LetsGoDBContext())
        {

        }

        public BaseEntityDAL(DbContext context) : base(context)
        {

        }
    }
}
