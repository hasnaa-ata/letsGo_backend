using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class RoutineDayModel
    {
    }

    public class RoutineDayModel<TModel> : BaseModel<RoutineDay, RoutineDay, RoutineDayDAL>
    {
        public IEnumerable<TModel> GetData(Guid? RoutineDayId = null, IEnumerable<Guid> RoutineDayId_lst = null, 
            Guid? RoutineId = null, string Day = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (RoutineDayId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineDayId", value: RoutineDayId));
            }
            if (RoutineDayId_lst != null && RoutineDayId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "RoutineDayId", values: RoutineDayId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (RoutineId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineId", value: RoutineId));
            }
            if (Day != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "Day", value: Day));
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

    public class RoutineDayViewModel : RoutineDay
    {

    }

    public class RoutineDayIndexViewModel : RoutineDayViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class RoutineDayDetailsViewModel : RoutineDayViewModel
    {

    }

    public class RoutineDayCreateBindModel : RoutineDay
    {

    }

    public class RoutineDayEditBindModel : RoutineDay
    {

    }

    public class RoutineDay_Create_Edit_Model
    {
        public RoutineDay Item { get; set; }
    }

    public class RoutineDayCreateModel : RoutineDay_Create_Edit_Model
    {

    }

    public class RoutineDayEditModel : RoutineDay_Create_Edit_Model
    {

    }

    #region DAL
    public class RoutineDayDAL : BaseEntityDAL<RoutineDay, RoutineDay, RoutineDayViewDAL>
    {

    }

    public class RoutineDayViewDAL : BaseEntityDAL<RoutineDay, RoutineDay, RoutineDayViewDAL>
    {

    }
    #endregion
}
