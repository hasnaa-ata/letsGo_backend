using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using Mawid.DataLayer.ViewEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LetsGo.BackEnd.Models
{
    public class CityModel<TModel> : BaseModel<City, CityView , CityDAL>
    {
        public IEnumerable<TModel> GetData(Guid? CityId = null, IEnumerable<Guid> CityId_lst = null,
            string CityName = null, string CityAltName = null, Guid? StateId = null, bool? IsBlock = null, 
            bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, 
            string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if(CityId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CityId", value: CityId));
            }
            if (CityId_lst != null && CityId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "CityId", values: CityId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (CityName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CityName", value: CityName));
            }
            if(CityAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CityAltName", value: CityAltName));
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

    public class CityViewModel : CityView
    {

    }

    public class CityIndexViewModel : CityViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class CityDetailsViewModel : CityViewModel
    {

    }

    public class CityCreateBindModel : City
    {

    }

    public class CityEditBindModel : City
    {

    }

    public class City_Create_Edit_Model
    {
        public City Item { get; set; }
        public ModelReference References { get; set; }

        public City_Create_Edit_Model()
        {
            References = new ModelReference();
        }

        public class ModelReference
        {
            public IEnumerable<CustomSelectListItem> Country_lst { get; set; }
        }

        public void Create_Edit_SetReference()
        {
            IEnumerable<Country> country_lst = new CountryModel<Country>().GetData(IsBlock: false, IsDeleted: false, IncludeReferences: "States");
            References.Country_lst = country_lst.Select(x => new CustomSelectListItem()
            {
                Text = CoreUtility.GetDDLText(x.CountryName, x.CountryAltName),
                AltText = CoreUtility.GetDDLAltText(x.CountryName, x.CountryAltName),
                Value = x.CountryId.ToString(),
                RelatedItems = x.States.Select(y => new CustomSelectListItem()
                {
                    Text = CoreUtility.GetDDLText(y.StateName, y.StateAltName),
                    AltText = CoreUtility.GetDDLAltText(y.StateName, y.StateAltName),
                    Value = y.StateId.ToString(),
                })
            });
        }
    }

    public class CityCreateModel : City_Create_Edit_Model
    {
        public void Create_SetReference()
        {
            Create_Edit_SetReference();
        }
    }

    public class CityEditModel : City_Create_Edit_Model
    {
        public void Edit_SetReference()
        {
            Create_Edit_SetReference();
        }
    }

    public class CityDAL : BaseEntityDAL<City, CityView, CityViewDAL>
    {

    }

    public class CityViewDAL : BaseEntityDAL<CityView, CityView, CityViewDAL>
    {

    }
}
