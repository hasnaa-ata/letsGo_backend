using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using Mawid.BackEnd.Models;
using DataLayer.Security.ViewEntity;
using System;
using System.Collections.Generic;
using LetsGo.BackEnd.Models;

namespace Security.Models
{
    public class UserRoleServiceAccessModel<TModel> : BaseModel<UserRoleServiceAccessView, UserRoleServiceAccessView, UserRoleServiceAccessViewDAL> where TModel : class
    {
        public IEnumerable<TModel> GetData(Guid? UserId = null, Guid? RoleId = null, Guid? ServiceId = null, Guid? AccessTypeId = null, string ControlTag = null,
            bool? IsBlock = null, bool? IsDeleted = false, string IncludeProperties = null,
            GenericDataFormat requestBody = null)
        {
            var filters = new List<GenericDataFormat.FilterItem>();

            if (UserId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserId", value: UserId));
            }

            if (RoleId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoleId", value: RoleId));
            }

            if (ServiceId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "ServiceId", value: ServiceId));
            }

            if (AccessTypeId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "AccessTypeId", value: AccessTypeId));
            }

            if (ControlTag != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "ControlTag", value: ControlTag));
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
            if (IncludeProperties != null)
            {
                requestBody.Includes = new GenericDataFormat.IncludeItems() { Properties = IncludeProperties };
            }
            if (requestBody.Filters == null)
            {
                requestBody.Filters = new List<GenericDataFormat.FilterItem>(filters);
            }
            else
            {
                requestBody.Filters.InsertRange(0, filters);
            }
            return (IEnumerable<TModel>)Get(requestBody);
        }

        //internal IEnumerable<TModel> GetUserPermissiom(Guid userId)
        //{
        //    return new UserRoleServiceAccessModel<TModel>().GetData(UserId: userId);
        //}

        public IEnumerable<UserRoleServiceAccessViewModel> GetSavedUserPermission()
        {
            List<UserRoleServiceAccessViewModel> userPermissions = null;
            //if (HttpContext.Current.Session != null && HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName] != null)
            //{
            //    //string json_UserPermission = (string)HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName];
            //    //userPermissions = (List<UserRoleServiceAccessViewModel>)Newtonsoft.Json.JsonConvert.DeserializeObject(json_UserPermission);
            //    userPermissions = (List<UserRoleServiceAccessViewModel>)HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName];
            //}
            //else
            //{
            //    var user = new UserViewModel().GetUserFromSession();
            //    userPermissions = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetUserPermissiom(user.UserId, true).ToList();
            //    SaveUserPermissionToLocalStorage(userPermissions, true);
            //}
            return userPermissions;
        }

        //public void SaveUserPermissionToLocalStorage(List<UserRoleServiceAccessViewModel> userPermissions, bool rememberMe)
        //{
        //    string json_UserPermission = Newtonsoft.Json.JsonConvert.SerializeObject(userPermissions);
        //    System.Web.Security.FormsAuthentication.SetAuthCookie(json_UserPermission, rememberMe);
        //    //for mashaweer test
        //    if (HttpContext.Current.Session != null)
        //    {
        //        HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName] = userPermissions;
        //    }
        //}

        //public bool RemoveUserPermissionSession()
        //{
        //    HttpContext.Current.Session.Remove(UserRoleServiceAccessViewModel.SessionName);
        //    return true;
        //}
    }

    public class UserRoleServiceAccessViewModel : UserRoleServiceAccessView
    {
        public const string SessionName = "UserRoleServiceAccess";
    }

    public class UserRoleServiceAccessDetailsViewModel : UserRoleServiceAccessViewModel
    {

    }


    #region DAL

    public class UserRoleServiceAccessViewDAL : BaseEntityDAL<UserRoleView, UserRoleView, UserRoleViewDAL>
    {

    }
    #endregion

}