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
    public class RoutineRouteTypeModel<TModel> : BaseModel<RoutineRouteType, RoutineRouteTypeView, RoutineRouteTypeDAL>
    {
        public IEnumerable<TModel> GetData(Guid? RoutineRouteTypeId = null, IEnumerable<Guid> RoutineRouteTypeId_lst = null,
            string RoutineRouteTypeName = null, string RoutineRouteTypeAltName = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (RoutineRouteTypeId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineRouteTypeId", value: RoutineRouteTypeId));
            }
            if (RoutineRouteTypeId_lst != null && RoutineRouteTypeId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "RoutineRouteTypeId", values: RoutineRouteTypeId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (RoutineRouteTypeName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineRouteTypeName", value: RoutineRouteTypeName));
            }
            if (RoutineRouteTypeAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineRouteTypeAltName", value: RoutineRouteTypeAltName));
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

    public class RoutineRouteTypeViewModel : RoutineRouteTypeView
    {

    }

    public class RoutineRouteTypeIndexViewModel : RoutineRouteTypeViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class RoutineRouteTypeDetailsViewModel : RoutineRouteTypeViewModel
    {

    }

    public class RoutineRouteTypeCreateBindModel : RoutineRouteType
    {

    }

    public class RoutineRouteTypeEditBindModel : RoutineRouteType
    {

    }

    public class RoutineRouteType_Create_Edit_Model
    {
        public RoutineRouteType Item { get; set; }
    }

    public class RoutineRouteTypeCreateModel : RoutineRouteType_Create_Edit_Model
    {

    }

    public class RoutineRouteTypeEditModel : RoutineRouteType_Create_Edit_Model
    {

    }

    public class RoutineRouteTypeDAL : BaseEntityDAL<RoutineRouteType, RoutineRouteTypeView, RoutineRouteTypeViewDAL>
    {

    }

    public class RoutineRouteTypeViewDAL : BaseEntityDAL<RoutineRouteTypeView, RoutineRouteTypeView, RoutineRouteTypeViewDAL>
    {

    }
}
