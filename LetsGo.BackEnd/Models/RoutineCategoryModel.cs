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
    public class RoutineCategoryModel<TModel> : BaseModel<RoutineCategory, RoutineCategoryView, RoutineCategoryDAL>
    {
        public IEnumerable<TModel> GetData(Guid? RoutineCategoryId = null, IEnumerable<Guid> RoutineCategoryId_lst = null,
            string RoutineCategoryName = null, string RoutineCategoryAltName = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (RoutineCategoryId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineCategoryId", value: RoutineCategoryId));
            }
            if (RoutineCategoryId_lst != null && RoutineCategoryId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "RoutineCategoryId", values: RoutineCategoryId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (RoutineCategoryName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineCategoryName", value: RoutineCategoryName));
            }
            if (RoutineCategoryAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineCategoryAltName", value: RoutineCategoryAltName));
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

    public class RoutineCategoryViewModel : RoutineCategoryView
    {

    }

    public class RoutineCategoryIndexViewModel : RoutineCategoryViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class RoutineCategoryDetailsViewModel : RoutineCategoryViewModel
    {

    }

    public class RoutineCategoryCreateBindModel : RoutineCategory
    {

    }

    public class RoutineCategoryEditBindModel : RoutineCategory
    {

    }

    public class RoutineCategory_Create_Edit_Model
    {
        public RoutineCategory Item { get; set; }
    }

    public class RoutineCategoryCreateModel : RoutineCategory_Create_Edit_Model
    {

    }

    public class RoutineCategoryEditModel : RoutineCategory_Create_Edit_Model
    {

    }

    public class RoutineCategoryDAL : BaseEntityDAL<RoutineCategory, RoutineCategoryView, RoutineCategoryViewDAL>
    {

    }

    public class RoutineCategoryViewDAL : BaseEntityDAL<RoutineCategoryView, RoutineCategoryView, RoutineCategoryViewDAL>
    {

    }
}
