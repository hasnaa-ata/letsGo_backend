using Classes.Utilities;
using DataLayer.Common;
using DataLayer.Security.TableEntity;
using GenericBackEndCore.Classes.Utilities;
using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationController : BaseApiController<Invitation, InvitationView, InvitationViewModel, InvitationDetailsViewModel,
     InvitationCreateBindModel, InvitationEditBindModel, InvitationCreateModel, InvitationEditModel, Invitation,
     InvitationModel<Invitation>, InvitationModel<InvitationView>>
    {
        private readonly UserManager<User> _userManager;
        public InvitationController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateInvitationAsync([FromBody] InvitationAddBody model, [FromRoute] Guid? notificationId, [FromRoute] Guid? taskId)
        {
            if (model != null)
            {
                User invUser = null;
                if (model.Email != null)
                {
                    invUser = await _userManager.FindByEmailAsync(model.Email);
                }
                else if (model.PhoneNumber != null)
                {
                    invUser = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
                }

                InvitationCreateBindModel item = new InvitationCreateBindModel()
                {
                    UserId = invUser.Id,
                    GroupId = model.GroupId,
                    InvitationStatus = "Pending"
                };

                return base.Create(item, notificationId, taskId);
            }

            var result = new GenericBackEndCore.Classes.Utilities.JsonResponse<bool>()
            {
                Result = false,
                Message = "No email or phone number provided",
                Status = 0,
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest
            };
            return BadRequest(result);
            
        }

        [HttpGet("UpdateInvitationStatus/{id:modelIdType}")]
        public IActionResult UpdateInvitationStatus(Guid id, [FromQuery] InvitationStatus status, [FromRoute] Guid? notificationId, [FromRoute] Guid? taskId)
        {
            Invitation invitation = new InvitationModel<Invitation>().Get(id);
            InvitationEditBindModel model = ChangeStatus(invitation, status);
            return base.Edit(id, model, notificationId, taskId);
        }

        private InvitationEditBindModel ChangeStatus(Invitation invitation, InvitationStatus status)
        {
            InvitationEditBindModel inv = CoreUtility.CopyObject<Invitation, InvitationEditBindModel>(invitation);
            switch (status)
            {
                case InvitationStatus.Accept:
                    inv.InvitationStatus = "Accepted";
                    break;
                case InvitationStatus.Reject:
                    inv.InvitationStatus = "Rejected";
                    break;
                default:
                    inv = null;
                    break;
            }
            return inv;
        }
    }
}
