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
    public class GroupStatusModel<TModel> : BaseModel<GroupStatus, GroupStatusView, GroupStatusDAL>
    {
        public IEnumerable<TModel> GetData(Guid? GroupStatusId = null, IEnumerable<Guid> GroupStatusId_lst = null,
            string GroupStatusName = null, string GroupStatusAltName = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (GroupStatusId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupStatusId", value: GroupStatusId));
            }
            if (GroupStatusId_lst != null && GroupStatusId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "GroupStatusId", values: GroupStatusId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (GroupStatusName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupStatusName", value: GroupStatusName));
            }
            if (GroupStatusAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupStatusAltName", value: GroupStatusAltName));
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

    public class GroupStatusViewModel : GroupStatusView
    {

    }

    public class GroupStatusIndexViewModel : GroupStatusViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class GroupStatusDetailsViewModel : GroupStatusViewModel
    {

    }

    public class GroupStatusCreateBindModel : GroupStatus
    {

    }

    public class GroupStatusEditBindModel : GroupStatus
    {

    }

    public class GroupStatus_Create_Edit_Model
    {
        public GroupStatus Item { get; set; }
    }

    public class GroupStatusCreateModel : GroupStatus_Create_Edit_Model
    {

    }

    public class GroupStatusEditModel : GroupStatus_Create_Edit_Model
    {

    }

    public class GroupStatusDAL : BaseEntityDAL<GroupStatus, GroupStatusView, GroupStatusViewDAL>
    {

    }

    public class GroupStatusViewDAL : BaseEntityDAL<GroupStatusView, GroupStatusView, GroupStatusViewDAL>
    {

    }
}
