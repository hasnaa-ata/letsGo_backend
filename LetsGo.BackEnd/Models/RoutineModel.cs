using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LetsGo.BackEnd.Models
{
    public class RoutineModel<TModel> : BaseModel<Routine, RoutineView, RoutineDAL>
    {
        public IEnumerable<TModel> GetData(Guid? RoutineId = null, Guid? UserId = null, IEnumerable<Guid> RoutineId_lst = null,
           string RoutineName = null, string RoutineAltName = null, bool? IsBlock = null,
           bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null,
           string IncludeReferences = null,
           GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (RoutineId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineId", value: RoutineId));
            }
            if (RoutineId_lst != null && RoutineId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "RoutineId", values: RoutineId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (RoutineName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineName", value: RoutineName));
            }
            if (UserId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserId", value: UserId));
            }
            if (RoutineAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "RoutineAltName", value: RoutineAltName));
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

    public class RoutineViewModel : RoutineView
    {

    }

    public class RoutineIndexViewModel : RoutineViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class RoutineDetailsViewModel : RoutineViewModel
    {
        public IEnumerable<RoutineDay> RoutineDays { get; set; }
    }

    public class RoutineCreateBindModel : Routine
    {

    }

    public class RoutineEditBindModel : Routine
    {

    }

    public class Routine_Create_Edit_Model
    {
        public Routine Item { get; set; }
        public ModelReference References { get; set; }
        public IEnumerable<RoutineDay> routineDays { get; set; }

        public Routine_Create_Edit_Model()
        {
            References = new ModelReference();
        }

        public class ModelReference
        {
            public IEnumerable<CustomSelectListItem> RoutineCategory_lst { get; set; }
            public IEnumerable<CustomSelectListItem> RoutineRouteType_lst { get; set; }
        }

        public void Create_Edit_SetReference()
        {
            IEnumerable<RoutineCategory> routineCategory_lst = new RoutineCategoryModel<RoutineCategory>().GetData(IsBlock: false, IsDeleted: false);
            References.RoutineCategory_lst = routineCategory_lst.Select(x => new CustomSelectListItem()
            {
                Text = CoreUtility.GetDDLText(x.RoutineCategoryName, x.RoutineCategoryAltName),
                AltText = CoreUtility.GetDDLAltText(x.RoutineCategoryName, x.RoutineCategoryAltName),
                Value = x.RoutineCategoryId.ToString()
            });

            IEnumerable<RoutineRouteType> routineRouteType_lst = new RoutineRouteTypeModel<RoutineRouteType>().GetData(IsBlock: false, IsDeleted: false);
            References.RoutineRouteType_lst = routineRouteType_lst.Select(x => new CustomSelectListItem()
            {
                Text = CoreUtility.GetDDLText(x.RoutineRouteTypeName, x.RoutineRouteTypeAltName),
                AltText = CoreUtility.GetDDLAltText(x.RoutineRouteTypeName, x.RoutineRouteTypeAltName),
                Value = x.RoutineRouteTypeId.ToString()
            });
        }
    }


    public class RoutineCreateModel : Routine_Create_Edit_Model
    {
        public void Create_SetReference()
        {
            Create_Edit_SetReference();
        }
    }

    public class RoutineEditModel : Routine_Create_Edit_Model
    {
        public void Edit_SetReference()
        {
            Create_Edit_SetReference();
        }
    }

    public class RoutineDAL : BaseEntityDAL<Routine, RoutineView, RoutineViewDAL>
    {

    }

    public class RoutineViewDAL : BaseEntityDAL<RoutineView, RoutineView, RoutineViewDAL>
    {

    }

    public class TimeLineRoutineDetailResponse
    {
        public Routine Routine { get; set; }
        public Group Group { get; set; }
        public List<GroupMember> GroupMembers { get; set; }
        public bool IsGroupRoutine { get; set; }
    }
}
