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
    public class CountryModel<TModel> : BaseModel<Country, CountryView, CountryDAL>
    {
        public IEnumerable<TModel> GetData(Guid? CountryId = null, IEnumerable<Guid> CountryId_lst = null,
            string CountryName = null, string CountryAltName = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (CountryId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CountryId", value: CountryId));
            }
            if (CountryId_lst != null && CountryId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "CountryId", values: CountryId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (CountryName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CountryName", value: CountryName));
            }
            if (CountryAltName != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "CountryAltName", value: CountryAltName));
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

    public class CountryViewModel : CountryView
    {

    }

    public class CountryIndexViewModel : CountryViewModel
    {
        public bool? EditPermission { get; set; }
        public bool? BlockPermission { get; set; }
    }

    public class CountryDetailsViewModel : CountryViewModel
    {

    }

    public class CountryCreateBindModel : Country
    {

    }

    public class CountryEditBindModel : Country
    {

    }

    public class Country_Create_Edit_Model
    {
        public Country Item { get; set; }
    }

    public class CountryCreateModel : Country_Create_Edit_Model
    {

    }

    public class CountryEditModel : Country_Create_Edit_Model
    {

    }

    public class CountryDAL : BaseEntityDAL<Country, CountryView, CountryViewDAL>
    {

    }

    public class CountryViewDAL : BaseEntityDAL<CountryView, CountryView, CountryViewDAL>
    {

    }
}
