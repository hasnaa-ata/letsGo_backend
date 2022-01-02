using DataLayer.Common;
using GenericBackEndCore.Classes.Common;
using GenericBackEndCore.Classes.Utilities;
using Mawid.BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LetsGo.BackEnd.Controllers
{
    [Authorize]
    public class BaseApiController<TDBEntity, TDBViewEntity, TIndexViewModel, TDetailsViewModel,
       TCreateBindModel, TEditBindModel, TCreateModel, TEditModel, TImportModel,
       TModel_TDBEntity, TModel_TDBViewEntity> : GenericBaseApiController<TDBEntity, TDBViewEntity, TDetailsViewModel,
       TCreateBindModel, TEditBindModel, TCreateModel, TEditModel, TImportModel,
       TModel_TDBEntity, TModel_TDBViewEntity, Guid, UserViewModel, Guid?, Guid?>
       where TDBEntity : class, new()
       where TDBViewEntity : class, new()
       where TDetailsViewModel : TDBViewEntity, new()
       where TCreateBindModel : TDBEntity, new()
       where TEditBindModel : TDBEntity, new()
       where TCreateModel : new()
       where TEditModel : new()
       where TModel_TDBEntity : new()
       where TModel_TDBViewEntity : new()
    {
        public IEnumerable<UserRoleServiceAccessViewModel> UserPermissions { get; set; }

        public override DBEnums.AccessType<Guid> AccessType
        {
            get
            {
                return DBEnums.AccessType<Guid>.Instance;
            }
        }

        public override AlertMessageResource MessageResource
        {
            get
            {
                return AlertMessageResource.GetInstance;
            }
        }


        public override bool FuncPreCreate(ref TCreateBindModel model, ref JsonResponse<TDBEntity> response)
        {
            //set primary key value
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBEntity));
            IEnumerable<string> keyNames = instance.GetPKColumns();
            if (keyNames != null && keyNames.SingleOrDefault() != null)
            {
                PropertyInfo pkProp = typeof(TCreateBindModel).GetProperty(keyNames.SingleOrDefault());
                pkProp.SetValue(model, Guid.NewGuid());
            }
            //set create user value
            Type modelType = typeof(TCreateBindModel);
            PropertyInfo createUserId = modelType.GetProperty("CreateUserId");
            if (createUserId != null)
            {
                createUserId.SetValue(model, Guid.Parse(User.Identity.GetUserId()));
            }
            //set create date value
            PropertyInfo createDate = modelType.GetProperty("CreateDate");
            if (createDate != null)
            {
                createDate.SetValue(model, DateTime.Now);
            }
            return true;
        }

        public override bool FuncPreCreate(ref IEnumerable<TCreateBindModel> item_lst, ref JsonResponse<IEnumerable<TDBEntity>> response)
        {
            //get primary key column name
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBEntity));
            IEnumerable<string> keyNames = instance.GetPKColumns();
            //get type of model
            Type modelType = typeof(TCreateBindModel);
            //get primary key propery
            PropertyInfo pkProp = (keyNames != null && keyNames.SingleOrDefault() != null) ?
                typeof(TCreateBindModel).GetProperty(keyNames.SingleOrDefault()) : null;
            PropertyInfo createUserId = modelType.GetProperty("CreateUserId");
            PropertyInfo createDate = modelType.GetProperty("CreateDate");
            bool hasPrimayKey = (pkProp != null), hasCreateUserIdProperty = (createUserId != null), hasCreateDateProperty = (createDate != null);

            if (hasPrimayKey || hasCreateUserIdProperty || hasCreateDateProperty)
            {
                item_lst = item_lst.Select(x =>
                {
                    if (hasPrimayKey)
                        pkProp.SetValue(x, Guid.Parse(User.Identity.GetUserId()));
                    if (hasCreateUserIdProperty)
                        createUserId.SetValue(x, SystemUser.UserId);
                    if (hasCreateDateProperty)
                        createDate.SetValue(x, DateTime.Now);
                    return x;
                });
            }

            return true;
        }

        public override bool FuncPreEdit(Guid id, ref TEditBindModel model, ref JsonResponse<TDBEntity> response)
        {
            if (model != null)
            {
                Type modelType = typeof(TEditBindModel);
                PropertyInfo modifyUserId = modelType.GetProperty("ModifyUserId");
                if (modifyUserId != null)
                {
                    modifyUserId.SetValue(model, Guid.Parse(User.Identity.GetUserId()));
                }
                PropertyInfo modifyDate = modelType.GetProperty("ModifyDate");
                if (modifyDate != null)
                {
                    modifyDate.SetValue(model, DateTime.Now);
                }
            }
            return (id != null && model != null);
        }

        public override IEnumerable<GenericRepositoryCore.Utilities.GenericDataFormat.FilterItem> GetUserServiceAccessConditionAsFilter(Guid serviceId, Guid accessTypeId)
        {
            return null;
        }

        public override void SetAccessPermission(ref IBaseAccessPermission userAccessPermission)
        {
            //throw new NotImplementedException();
        }

        public override bool UserHasPermission(Guid serviceId, Guid accessTypeId)
        {
            return true;
        }


    }
}
