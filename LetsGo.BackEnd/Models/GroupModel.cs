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
    public class GroupModel<TModel> : BaseModel<Group, GroupView, GroupDAL>
    {
        public IEnumerable<TModel> GetData(Guid? GroupId = null, IEnumerable<Guid> GroupId_lst = null,
            string GroupName = null, string GroupAltName = null, Guid? StateId = null, bool? IsBlock = null,
            bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null,
            string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (GroupId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupId", value: GroupId));
            }
            if (GroupId_lst != null && GroupId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "GroupId", values: GroupId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (GroupName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupName", value: GroupName));
            }
            if (GroupAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupAltName", value: GroupAltName));
            }
            if (StateId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "StateId", value: StateId));
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
    public class FriendAddModel
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }

    public class GroupViewModel : GroupView
    {

    }

    public class GroupIndexViewModel : GroupViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class GroupDetailsViewModel : GroupViewModel
    {
        public IEnumerable<GroupMember> Members { get; set; }
        public IEnumerable<string> Media { get; set; }
        public Guid GroupAdminId { get; set; }
    }

    public class GroupCreateBindModel : Group
    {

    }

    public class GroupEditBindModel : Group
    {

    }

    public class Group_Create_Edit_Model
    {
        public Group Item { get; set; }
        public ModelReference References { get; set; }
        //public IEnumerable<UserGroupViewModel> UserGroups { get; set; }

        public Group_Create_Edit_Model()
        {
            References = new ModelReference();
        }

        public class ModelReference
        {
            public IEnumerable<CustomSelectListItem> GroupStatus_lst { get; set; }
            public IEnumerable<CustomSelectListItem> Routine_lst { get; set; }
        }

        public void Create_Edit_SetReference(Guid userId)
        {
            IEnumerable<GroupStatus> country_lst = new GroupStatusModel<GroupStatus>().GetData(IsBlock: false, IsDeleted: false);
            References.GroupStatus_lst = country_lst.Select(x => new CustomSelectListItem()
            {
                Text = CoreUtility.GetDDLText(x.GroupStatusName, x.GroupStatusAltName),
                AltText = CoreUtility.GetDDLAltText(x.GroupStatusName, x.GroupStatusAltName),
                Value = x.GroupStatusId.ToString()
            });
            IEnumerable<Routine> routine_lst = new RoutineModel<Routine>()
                .GetData(IsBlock: false, UserId: userId , IsDeleted: false, IncludeReferences: "Groups")
                .Where(x => x.Groups == null || x.Groups.Count() == 0);
            References.Routine_lst = routine_lst.Select(x => new CustomSelectListItem()
            {
                Text = CoreUtility.GetDDLText(x.RoutineName, x.RoutineAltName),
                AltText = CoreUtility.GetDDLAltText(x.RoutineName, x.RoutineAltName),
                Value = x.RoutineId.ToString()
            });
        }
    }

    public class GroupCreateModel : Group_Create_Edit_Model
    {
        public void CreateSetReference(Guid userId)
        {
            Create_Edit_SetReference(userId);
        }
    }

    public class GroupEditModel : Group_Create_Edit_Model
    {
        public void EditSetReference(Guid userId)
        {
            Create_Edit_SetReference(userId);
        }
    }

    public class GroupDAL : BaseEntityDAL<Group, GroupView, GroupViewDAL>
    {

    }

    public class GroupViewDAL : BaseEntityDAL<GroupView, GroupView, GroupViewDAL>
    {

    }
}
