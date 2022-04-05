using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using LetsGo.DataLayer.TableEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class FirebaseTokenModel<TModel> : BaseModel<FirebaseToken, FirebaseToken, FirebaseTokenDAL>
    {
        public IEnumerable<TModel> GetData(Guid? FirebaseTokenId = null, IEnumerable<Guid> FirebaseTokenId_lst = null,
           Guid? UserId = null, string Platform = null, string Token = null,
           bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
           GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (FirebaseTokenId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "FirebaseTokenId", value: FirebaseTokenId));
            }
            if (FirebaseTokenId_lst != null && FirebaseTokenId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "FirebaseTokenId", values: FirebaseTokenId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (UserId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserId", value: UserId));
            }
            if (Platform != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "Platform", value: Platform));
            }
            if (Token != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "Token", value: Token));
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

    public class FirebaseTokenDAL : BaseEntityDAL<FirebaseToken, FirebaseToken, FirebaseTokenDAL>
    {

    }
}
