using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class GroupMediaModel<TModel> : BaseModel<GroupMedia, GroupMedia, GroupMediaDAL>
    {
        public IEnumerable<TModel> GetData(Guid? GroupMediaId = null, IEnumerable<Guid> GroupMediaId_lst = null,
            Guid? GroupId = null, string GroupMediaURL = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (GroupMediaId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupMediaId", value: GroupMediaId));
            }
            if (GroupMediaId_lst != null && GroupMediaId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "GroupMediaId", values: GroupMediaId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (GroupId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupId", value: GroupId));
            }
            if (GroupMediaURL != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupMediaURL", value: GroupMediaURL));
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


    public class GroupMediaDAL : BaseEntityDAL<GroupMedia, GroupMedia, GroupMediaDAL>
    {

    }
}
