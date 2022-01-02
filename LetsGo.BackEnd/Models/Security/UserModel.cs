using System;
using System.Collections.Generic;
using System.Linq;
using Mawid.DataLayer.ViewEntity;
using GenericRepositoryCore.Utilities;
using GenericBackEndCore.Classes.Utilities;
using DataLayer.Security.TableEntity;
using System.ComponentModel.DataAnnotations;
using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.ViewEntity;
using GenericBackEndCore.Classes.Common;
using Security.Models;
using Classes.Utilities;

namespace Mawid.BackEnd.Models
{
    public class UserModel<TModel> : BaseModel<User,UserView,UserDAL> where TModel : class
    {
        public IEnumerable<TModel> GetData(Guid? UserId = null, IEnumerable<Guid> UserId_lst = null, string UserName = null, string Password = null,
            Guid? UserTypeId = null, bool? AllowAccess = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (UserId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserId", value: UserId));
            }
            if (UserId_lst != null && UserId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "UserId", values: UserId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (UserName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserName", value: UserName));
            }
            if (Password != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "Password", value: Password));
            }
            if (UserTypeId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserTypeId", value: UserTypeId));
            }
            if (AllowAccess != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "AllowAccess", value: AllowAccess));
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

    public class UserModel
    {
        //public void ChangePassword(object userId, string password, Guid modifyUserId)
        //{
        //    var filters = new List<GenericDataFormat.FilterItem>();
        //    filters.Add(CoreUtility.GetFilter(key: "UserId", value: userId));

        //    var includes = new GenericDataFormat.IncludeItems() { References = "UserService,UserServiceAccess,UserRole" };
        //    var editUser = new UserModel<User>().Get(new GenericDataFormat() { Filters = filters, Includes = includes }).SingleOrDefault();
        //    editUser.Password = UserModel.GetHashPassword(editUser.UserName, password);
        //    editUser.ModifyUserId = modifyUserId;
        //    editUser.ModifyDate = DateTime.Now;
        //    new UserModel<User>().Update(editUser, editUser.UserId);
        //}

        public static string GetHashPassword(string userName, string password)
        {
            return SecurityMethods.Hashing(userName, password);
        }

        internal void Get_Create_Modify_User(Guid createUserId, Guid? modifyUserId, ref string CreateUser_FullName, ref string CreateUser_FullAltName, ref string ModifyUser_FullName, ref string ModifyUser_FullAltName)
        {
            List<Guid> userId_lst = new List<Guid>();
            if (createUserId != null)
            {
                userId_lst.Add(createUserId);
            }

            if (modifyUserId.HasValue && createUserId.ToString() != modifyUserId.Value.ToString())
            {
                userId_lst.Add(modifyUserId.Value);
            }
            IEnumerable<UserView> user_lst = new UserModel<UserView>().GetData(UserId_lst: userId_lst, fromView: true);
            var createUser = user_lst.SingleOrDefault(x => x.UserId == createUserId);
            if(createUser != null)
            {
                CreateUser_FullName = createUser.UserFullName;
                CreateUser_FullAltName = createUser.UserAltFullName;
            }
            if (modifyUserId.HasValue)
            {
                var modifyUser = user_lst.SingleOrDefault(x => x.UserId == modifyUserId);
                if(modifyUser != null)
                {
                    ModifyUser_FullName = modifyUser.UserFullName;
                    ModifyUser_FullAltName = modifyUser.UserAltFullName;
                }
            }
        }

        public UserViewModel GetUserFromSession()
        {
            UserViewModel cUser = null;
            //if (HttpContext.Current.Session != null && HttpContext.Current.Session[UserViewModel.SessionName] != null)
            //{
            //    cUser = (UserViewModel)HttpContext.Current.Session[UserViewModel.SessionName];
            //}
            return cUser;
        }
    }

    public class UserViewModel : UserView
    {
        //public const string SessionName = "CurrentUser";

        //public UserViewModel Login()
        //{
        //    IEnumerable<UserViewModel> user_lst = new UserModel<UserViewModel>().GetData(UserName: UserName, fromView: true);
        //    if (user_lst != null && user_lst.SingleOrDefault() != null)
        //    {
        //        UserViewModel user = user_lst.SingleOrDefault();
        //        if (user.Password == UserModel.GetHashPassword(UserName, Password))
        //        {
        //            return user;
        //        }
        //    }
        //    return null;
        //}

        //public void SaveUserToLocalStorage(bool rememberMe)
        //{
        //    System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserName, rememberMe);
        //    System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserId.ToString(), rememberMe);
        //    if (HttpContext.Current.Session != null)
        //    {
        //        HttpContext.Current.Session[UserViewModel.SessionName] = this;
        //    }
        //}

        

        //public bool RemoveUserSession()
        //{
        //    HttpContext.Current.Session.Remove(UserViewModel.SessionName);
        //    var tUser = (UserViewModel)HttpContext.Current.Session[UserViewModel.SessionName];
        //    return true;
        //}
    }

    public class UserIndexViewModel : UserView
    {

    }

    public class UserDetailsViewModel : UserViewModel
    {
        public IEnumerable<UserRoleViewModel> UserRoles { get; set; }
    }

    public class UserCreateBindModel : User
    {
        public int? Employee_aux_id { get; set; }
    }

    public class UserEditBindModel : User
    {
    }

    public class UserEditModel
    {
        public User EditItem { get; set; }
        public IEnumerable<Classes.Utilities.CustomSelectListItem> Employee { get; set; }

    }

    public class RegisterViewModel
    {
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserFullName { get; set; }

        [Required]
        public string UserAltFullName { get; set; }

        [StringLength(100)]
        public string JobTitle { get; set; }

        [StringLength(100)]
        public string WorkPlace { get; set; }

    }

    public class UserLoginViewModel
    {
        [Required, StringLength(50), EmailAddress]
        public string Username { get; set; }
        [Required, StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
    }

    #region DAL
    public class UserDAL : BaseEntityDAL<User, UserView, UserViewDAL>
    {
        
    }

    public class UserViewDAL : BaseEntityDAL<UserView, UserView, UserViewDAL>
    {

    }
    #endregion
}