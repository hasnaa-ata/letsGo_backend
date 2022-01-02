using LetsGo.DataLayer.ViewEntity;
using LetsGo.DataLayer.TableEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Common;
using GenericBackEndCore.Classes.Utilities;
using GenericRepositoryCore.Utilities;
using DataLayer.Security.TableEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LetsGo.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : BaseApiController<Group, GroupView, GroupIndexViewModel, GroupDetailsViewModel,
     GroupCreateBindModel, GroupEditBindModel, GroupCreateModel, GroupEditModel, Group,
     GroupModel<Group>, GroupModel<GroupView>>
    {
        private readonly UserManager<User> _userManager;

        public GroupController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        public override IActionResult FuncPostInitCreateView(ref GroupCreateModel model, Guid? notificationId, Guid? taskId, ref JsonResponse<GroupCreateModel> response)
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            model.CreateSetReference(userId);

            return base.FuncPostInitCreateView(ref model, notificationId, taskId, ref response);
        }

        public override IActionResult FuncPostInitEditView(Guid id, ref GroupEditModel model, Guid? notificationId, Guid? taskId, ref JsonResponse<GroupEditModel> response)
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            model.EditSetReference(userId);
            return base.FuncPostInitEditView(id, ref model, notificationId, taskId, ref response);
        }

        public override bool FuncPreGetGridView(ref GenericDataFormat options, ref JsonResponse<PaginationResult<object>> response)
        {
            options.Filters.Add(new GenericDataFormat.FilterItem()
            {
                LogicalOperation = GenericDataFormat.LogicalOperation.And,
                Operation = GenericDataFormat.FilterOperation.Equal,
                Property = "UserId",
                Value = Guid.Parse(User.Identity.GetUserId())
            });
            return base.FuncPreGetGridView(ref options, ref response);
        }

        public override bool FuncPreCreate(ref GroupCreateBindModel model, ref JsonResponse<Group> response)
        {
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            model.UserGroups = model.UserGroups.Select(x => {
                x.CreateDate = DateTime.Now; 
                x.CreateUserId = currentUserId; 
                if(x.UserId == currentUserId)
                {
                    x.RoleId = RoleIds.GroupAdmin;
                }
                else
                {
                    x.RoleId = RoleIds.User;
                }
                return x;
            }).ToList();
            return base.FuncPreCreate(ref model, ref response);
        }

        public override IActionResult FuncPostDetailsView(bool success, Guid id, ref GroupDetailsViewModel model, Guid? notificationId, ref JsonResponse<GroupDetailsViewModel> response)
        {
            Guid? userId = Guid.Parse(User.Identity.GetUserId());
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            List<UserGroup> userGroups = userGroupModel.GetData(GroupId: id, IncludeReferences: "Role").ToList();
            List<GroupMember> members = new List<GroupMember>();
            if (userGroups != null && userGroups.Any())
            {
                foreach (UserGroup gp in userGroups)
                {
                    var friend = _userManager.FindByIdAsync(gp.UserId.ToString()).Result;
                    GroupMember member = CoreUtility.CopyObject<User, GroupMember>(friend);
                    if(gp.Role.Id == RoleIds.GroupAdmin)
                    {
                        model.GroupAdminId = member.Id;
                    }
                    if (friend.Id != userId && !members.Contains(member))
                    {
                        //if (gp.Role.Id == RoleIds.GroupAdmin)
                        //{
                        //    member.GroupRole = "Admin";
                        //    model.GroupAdminId = member.Id;
                        //}
                        //else
                        //{
                        //    member.GroupRole = "User";
                        //}
                        members.Add(member);
                    }
                }
            }
            model.Members = members;

            return base.FuncPostDetailsView(success, id, ref model, notificationId, ref response);
        }

        [HttpGet("addFriends/{groupId}")]
        [Authorize]
        public IActionResult AddFriendsToGroup( Guid groupId,
            [FromQuery(Name = "userIds")] List<Guid> userIds
            )
        {
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            var currentUserRole = userGroupModel.GetData(GroupId: groupId, UserId: currentUserId, IncludeReferences: "Role").Select(x => x.Role).FirstOrDefault();
            if (currentUserRole.Id != RoleIds.GroupAdmin)
            {
                return Unauthorized(
                   new JsonResponse<bool>()
                   {
                       Status = 0,
                       HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                       Result = false,
                       Errors = new List<String>() {
                        "Unauthorized"
                       },
                       Message = "Unauthorized"
                   });
            }
            var userGroups = userIds.Select(x => new UserGroup()
            {
                CreateDate = DateTime.Now,
                CreateUserId = currentUserId,
                RoleId = RoleIds.User,
                UserId = x,
                GroupId = groupId,
                IsBlock = false,
                IsDeleted = false,
            });
            var model = userGroupModel.Insert(userGroups);
            if(model != null)
            {
                return Ok(
                    new JsonResponse<bool>() { 
                        Status = 1,
                        HttpStatusCode = System.Net.HttpStatusCode.OK,
                        Result = true,
                        Message = "added friends successfully"
                    });
            }
            else
            {
                return BadRequest(
                    new JsonResponse<bool>()
                    {
                        Status = 0,
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                        Result = false,
                        Errors = new List<String>() { 
                        "couldn't add friends to the group"
                        },
                        Message = "couldn't add friends to the group"
                    });
            }
        }

        [HttpGet("addFriend/{groupId}")]
        [Authorize]
        public async Task<IActionResult> AddFriendByEmailToGroupAsync(Guid groupId,
        [FromQuery(Name = "email")] string email)
        {
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            var currentUserRole = userGroupModel.GetData(GroupId: groupId, UserId: currentUserId, IncludeReferences: "Role").Select(x => x.Role).FirstOrDefault();
            if (currentUserRole.Id != RoleIds.GroupAdmin)
            {
                return Unauthorized(
                   new JsonResponse<bool>()
                   {
                       Status = 0,
                       HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                       Result = false,
                       Errors = new List<String>() {
                        "Unauthorized"
                       },
                       Message = "Unauthorized"
                   });
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(
                   new JsonResponse<bool>()
                   {
                       Status = 0,
                       HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                       Result = false,
                       Errors = new List<String>() {
                        "Couldn't find user"
                       },
                       Message = "Couldn't find user"
                   });
            }
            var existedUser = userGroupModel.GetData(GroupId: groupId, UserId: user.Id, IncludeReferences: "Role").FirstOrDefault();
            if(existedUser != null)
            {
                return BadRequest(
                   new JsonResponse<bool>()
                   {
                       Status = 0,
                       HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                       Result = false,
                       Errors = new List<String>() {
                        "user already in group"
                       },
                       Message = "user already in group"
                   });
            }

            var userGroups = new UserGroup()
            {
                CreateDate = DateTime.Now,
                CreateUserId = currentUserId,
                RoleId = RoleIds.User,
                UserId = user.Id,
                GroupId = groupId,
                IsBlock = false,
                IsDeleted = false,
            };
            var model = userGroupModel.Insert(userGroups);
            if (model != null)
            {
                return Ok(
                    new JsonResponse<bool>()
                    {
                        Status = 1,
                        HttpStatusCode = System.Net.HttpStatusCode.OK,
                        Result = true,
                        Message = "added friend successfully"
                    });
            }
            else
            {
                return BadRequest(
                    new JsonResponse<bool>()
                    {
                        Status = 0,
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                        Result = false,
                        Errors = new List<String>() {
                        "couldn't add friend to the group"
                        },
                        Message = "couldn't add friend to the group"
                    });
            }
        }

        [HttpGet("removeFriend")]
        [Authorize]
        public IActionResult RemoveFriendFromGroup(
            [FromQuery(Name = "GroupId")] Guid groupId,
            [FromQuery(Name = "UserId")] Guid userId
            )
        {
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            var currentUserRole = userGroupModel.GetData(GroupId: groupId, UserId: currentUserId, IncludeReferences: "Role").Select(x => x.Role).FirstOrDefault();
            if(currentUserRole.Id != RoleIds.GroupAdmin)
            {
                return Unauthorized(
                   new JsonResponse<bool>()
                   {
                       Status = 0,
                       HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                       Result = false,
                       Errors = new List<String>() {
                        "Unauthorized"
                       },
                       Message = "Unauthorized"
                   });
            }

            var model = userGroupModel.GetData(GroupId: groupId, UserId: userId).FirstOrDefault();
            if (model != null)
            {
                bool deleted = userGroupModel.Delete(model.UserGroupId);
                if (deleted)
                {
                   return Ok(
                    new JsonResponse<bool>()
                    {
                        Status = 1,
                        HttpStatusCode = System.Net.HttpStatusCode.OK,
                        Result = true,
                        Message = "removed friend successfully"
                    });
                }

                return BadRequest(
                new JsonResponse<bool>()
                {
                    Status = 0,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Result = true,
                    Message = "couldn't delete friend"
                });
            }
            else
            {
                return BadRequest(
                    new JsonResponse<bool>()
                    {
                        Status = 0,
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                        Result = false,
                        Errors = new List<String>() {
                        "couldn't find friend"
                        },
                        Message = "couldn't find friend"
                    });
            }
        }


        [HttpGet("leaveGroup")]
        [Authorize]
        public IActionResult LeaveGroup(
            [FromQuery(Name = "GroupId")] Guid groupId,
            [FromQuery(Name = "NewAdminId")] Guid? newAdminId
            )
        {
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            var currentUserRole = userGroupModel.GetData(GroupId: groupId, UserId: currentUserId, IncludeReferences: "Role").Select(x => x.Role).FirstOrDefault();
            if (currentUserRole.Id == RoleIds.GroupAdmin && newAdminId != null )
            {
                var newAdminUserGroup = userGroupModel.GetData(GroupId: groupId, UserId: newAdminId).FirstOrDefault();
                if(newAdminUserGroup != null)
                {
                    newAdminUserGroup.RoleId = RoleIds.GroupAdmin;
                    userGroupModel.Update(newAdminUserGroup.UserGroupId, newAdminUserGroup);
                }
                else
                {
                    return BadRequest(
                       new JsonResponse<bool>()
                       {
                           Status = 0,
                           HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                           Result = false,
                           Errors = new List<String>() {
                                            "Couldn't find user to make admin"
                           },
                           Message = "Couldn't find user to make admin"
                       });
                }
            }
            else
            {
                return Unauthorized(
                   new JsonResponse<bool>()
                   {
                       Status = 0,
                       HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
                       Result = false,
                       Errors = new List<String>() {
                        "Unauthorized"
                       },
                       Message = "Unauthorized"
                   });
            }

            var model = userGroupModel.GetData(GroupId: groupId, UserId: currentUserId).FirstOrDefault();
            if (model != null)
            {
                bool deleted = userGroupModel.Delete(model.UserGroupId);
                if (deleted)
                {
                    return Ok(
                     new JsonResponse<bool>()
                     {
                         Status = 1,
                         HttpStatusCode = System.Net.HttpStatusCode.OK,
                         Result = true,
                         Message = "left group successfully"
                     });
                }

                return BadRequest(
                new JsonResponse<bool>()
                {
                    Status = 0,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Result = true,
                    Message = "couldn't leave group"
                });
            }
            else
            {
                return BadRequest(
                    new JsonResponse<bool>()
                    {
                        Status = 0,
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                        Result = false,
                        Errors = new List<String>() {
                        "couldn't find user"
                        },
                        Message = "couldn't find user"
                    });
            }
        }
    }
}
