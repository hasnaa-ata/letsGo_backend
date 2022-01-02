using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class UserGroupModel<TModel> : BaseModel<UserGroup, UserGroupView, UserGroupDAL>
    {
        public IEnumerable<TModel> GetData(Guid? UserGroupId = null, IEnumerable<Guid> UserGroupId_lst = null,
            Guid? GroupId = null, Guid? UserId = null, Guid? RoleId = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (UserGroupId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserGroupId", value: UserGroupId));
            }
            if (UserGroupId_lst != null && UserGroupId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "UserGroupId", values: UserGroupId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (GroupId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupId", value: GroupId));
            }
            if (UserId != null)
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

    public class UserGroupViewModel
    {
        public Guid UserGroupId { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public string UserFullName { get; set; }

        public string UserAltFullName { get; set; }

        public string RoleName { get; set; }

        public string RoleAltName { get; set; }
    }

    public class UserGroupDetailsViewModel : UserGroupViewModel
    {

    }


    public class UserGroupDAL : BaseEntityDAL<UserGroup, UserGroupView, UserGroupViewDAL>
    {

    }

    public class UserGroupViewDAL : BaseEntityDAL<UserGroupView, UserGroupView, UserGroupViewDAL>
    {

    }

}
