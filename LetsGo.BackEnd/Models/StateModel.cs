using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using Mawid.DataLayer.ViewEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class StateModel<TModel> : BaseModel<State, StateView, StateDAL>
    {
        public IEnumerable<TModel> GetData(Guid? StateId = null, IEnumerable<Guid> StateId_lst = null,
            string StateName = null, string StateAltName = null, Guid? CountryId = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (StateId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "StateId", value: StateId));
            }
            if (StateId_lst != null && StateId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "StateId", values: StateId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (StateName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "StateName", value: StateName));
            }
            if (StateAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "StateAltName", value: StateAltName));
            }
            if (CountryId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CountryId", value: CountryId));
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

    public class StateViewModel : StateView
    {

    }

    public class StateIndexViewModel : StateViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class StateDetailsViewModel : StateViewModel
    {

    }

    public class StateCreateBindModel : State
    {

    }

    public class StateEditBindModel : State
    {

    }

    public class State_Create_Edit_Model
    {
        public State Item { get; set; }
        public ModelReference References { get; set; }

        public State_Create_Edit_Model()
        {
            References = new ModelReference();
        }

        public class ModelReference
        {
            public IEnumerable<CustomSelectListItem> Country_lst { get; set; }
        }

        public void Create_Edit_SetReference()
        {
            References.Country_lst = new CountryModel<Country>().GetData(IsBlock: false, IncludeProperties: "CountryId,CountryName,CountryAltName")
                .Select(x => new CustomSelectListItem() { Text = CoreUtility.GetDDLText(x.CountryName, x.CountryAltName), Value = x.CountryId.ToString() });
        }

    }

    public class StateCreateModel : State_Create_Edit_Model
    {
        public void Create_SetReference()
        {
            Create_Edit_SetReference();
        }
    }

    public class StateEditModel : State_Create_Edit_Model
    {
        public void Edit_SetReference()
        {
            Create_Edit_SetReference();
        }
    }

    public class StateDAL : BaseEntityDAL<State, StateView, StateViewDAL>
    {

    }

    public class StateViewDAL : BaseEntityDAL<StateView, StateView, StateViewDAL>
    {

    }
}
