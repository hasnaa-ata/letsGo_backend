using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using DataLayer.Security.TableEntity;
using DataLayer.Security.ViewEntity;
using System;
using System.Collections.Generic;
using LetsGo.BackEnd.Models;

namespace Security.Models
{
    public class UserRoleModel<TModel> : BaseModel<UserRole,UserRoleView,UserRoleDAL> where TModel : class
    {
        internal IEnumerable<TModel> GetData(Guid? UserRoleId = null, Guid? UserId = null, Guid? RoleId = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            var filters = new List<GenericDataFormat.FilterItem>();
            if (UserRoleId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserRoleId", value: UserRoleId));
            }
            if(UserId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserId", value: UserId));
            }
            if (RoleId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoleId", value: RoleId));
            }
            if (IsBlock != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "IsBlock", value: IsBlock));
            }
            if (IsDeleted != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "IsDeleted", value: IsDeleted));
            }
            if (requestBody == null)
            {
                requestBody = new GenericDataFormat();
            }
            if (IncludeProperties != null || IncludeReferences != null)
            {
                requestBody.Includes = new GenericDataFormat.IncludeItems() { Properties = IncludeProperties, References = IncludeReferences };
            }
            if (requestBody.Filters == null)
            {
                requestBody.Filters = new List<GenericDataFormat.FilterItem>(filters);
            }
            else
            {
                requestBody.Filters.InsertRange(0, filters);
            }
            if (fromView)
            {
                return (IEnumerable<TModel>)GetView(requestBody);
            }
            else
            {
                return (IEnumerable<TModel>)Get(requestBody);
            }
        }
    }
    public class UserRoleViewModel : UserRoleView
    {


    }
    public class UserRoleIndexViewModel : UserRoleView
    {

    }
    public class UserRoleDetailsViewModel : UserRoleViewModel
    {

    }

    #region DAL
    public class UserRoleDAL : BaseEntityDAL<UserRole, UserRoleView, UserRoleViewDAL>
    {
       
    }

    public class UserRoleViewDAL : BaseEntityDAL<UserRoleView, UserRoleView, UserRoleViewDAL>
    {

    }
    #endregion
}