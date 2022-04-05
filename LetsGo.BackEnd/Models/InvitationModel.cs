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
    public class InvitationModel<TModel> : BaseModel<Invitation, InvitationView, InvitationDAL>
    {
        public IEnumerable<TModel> GetData(Guid? InvitationId = null, IEnumerable<Guid> InvitationId_lst = null,
            Guid? UserId = null, Guid? GroupId = null, string InvitationStatus = null,
            bool? IsBlock = null, bool? IsDeleted = false, bool fromView = false, string IncludeProperties = null, string IncludeReferences = null,
            GenericDataFormat requestBody = null)
        {
            List<GenericDataFormat.FilterItem> filters = new List<GenericDataFormat.FilterItem>();
            if (InvitationId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "InvitationId", value: InvitationId));
            }
            if (InvitationId_lst != null && InvitationId_lst.Any())
            {
                filters.AddRange(CoreUtility.GetFilter(key: "InvitationId", values: InvitationId_lst.Select(x => (object)x), LogicalOperation: "Or"));
            }
            if (UserId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "UserId", value: UserId));
            }
            if (GroupId != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "GroupId", value: GroupId));
            }
            if (InvitationStatus != null)
            {
                filters.Add(CoreUtility.GetFilter(key: "InvitationStatus", value: InvitationStatus));
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

    public class InvitationViewModel : InvitationView
    {

    }

    public class InvitationDetailsViewModel : InvitationViewModel
    {
        public GroupDetailsViewModel Group { get; set; }
    }

    public class InvitationCreateBindModel : Invitation
    {

    }

    public class InvitationEditBindModel : Invitation
    {

    }

    public class Invitation_Create_Edit_Model
    {
        public Invitation Item { get; set; }
    }

    public class InvitationCreateModel : Invitation_Create_Edit_Model
    {

    }

    public class InvitationEditModel : Invitation_Create_Edit_Model
    {

    }

    public class InvitationDAL : BaseEntityDAL<Invitation, InvitationView, InvitationViewDAL>
    {
       
    }

    public class InvitationViewDAL : BaseEntityDAL<InvitationView, InvitationView, InvitationViewDAL>
    {

    }


    public class InvitationAddBody
    {
        public Guid GroupId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; } 
    }

    public enum InvitationStatus
    {
        Pending = 1, Accept = 2, Reject = 3
    }
}
