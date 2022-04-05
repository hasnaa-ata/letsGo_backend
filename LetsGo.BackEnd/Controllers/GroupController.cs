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
using PlusAction.BackEnd.Common;
using System.Net;
using Newtonsoft.Json;
using LetsGo.BackEnd.Utilities;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LetsGo.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : BaseApiController<Group, GroupView, GroupIndexViewModel, GroupDetailsViewModel,
     GroupCreateBindModel, GroupEditBindModel, GroupCreateModel, GroupEditModel, Group,
     GroupModel<Group>, GroupModel<GroupView>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public GroupController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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
            //Guid? userId = Guid.Parse(User.Identity.GetUserId());
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            GroupMediaModel<GroupMedia> groupMediaModel = new GroupMediaModel<GroupMedia>();
            List<UserGroup> userGroups = userGroupModel.GetData(GroupId: id, IncludeReferences: "Role").ToList();
            List<GroupMember> members = new List<GroupMember>();
            if (userGroups != null && userGroups.Any())
            {
                foreach (UserGroup user_gp in userGroups)
                {
                    var friend = _userManager.FindByIdAsync(user_gp.UserId.ToString()).Result;
                    GroupMember member = CoreUtility.CopyObject<User, GroupMember>(friend);
                    if(user_gp.Role.Id == RoleIds.GroupAdmin)
                    {
                        model.GroupAdminId = member.Id;
                    }
                    if (!members.Contains(member))
                    {
                        if (user_gp.Role.Id == RoleIds.GroupAdmin)
                        {
                            member.GroupRole = "Admin";
                        }
                        else
                        {
                            member.GroupRole = "User";
                        }
                        members.Add(member);
                    }
                }
            }
            model.Members = members;

            List<GroupMedia> groupMedias = groupMediaModel.GetData(GroupId: id).ToList();
            List<string> mediaUrls = new List<string>();
            if (groupMedias != null && groupMedias.Any())
            {
                //mediaUrls = groupMedias
                //    .Select(m => _configuration["AppUrl"] + "/api/group/groupMedia/" + m.GroupMediaId.ToString())
                //    .ToList();

                mediaUrls = groupMedias
                    .Select(m => "http://192.168.56.1:3000/api/group/groupMedia/" + m.GroupMediaId.ToString())
                    .ToList();
            }

            model.Media = mediaUrls;

            return base.FuncPostDetailsView(success, id, ref model, notificationId, ref response);
        }

        [HttpGet("addFriends/{groupId}")]
        [Authorize]
        public IActionResult AddFriendsToGroup(Guid groupId, [FromQuery(Name = "userIds")] List<Guid> userIds)
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
                       HttpStatusCode = HttpStatusCode.Unauthorized,
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
            //else
            //{
            //    return Unauthorized(
            //       new JsonResponse<bool>()
            //       {
            //           Status = 0,
            //           HttpStatusCode = System.Net.HttpStatusCode.Unauthorized,
            //           Result = false,
            //           Errors = new List<String>() {
            //            "Unauthorized"
            //           },
            //           Message = "Unauthorized"
            //       });
            //}

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

        [AllowAnonymous]
        [HttpGet("groupImage/{groupId}")]
        public IActionResult ImageAsync(Guid groupId)
        {
            GroupModel<Group> groupModel = new GroupModel<Group>();
            Group group = groupModel.Get(groupId);
            string path = group.GroupImageURL;
            string contentType = group.ImageContentType;
            var image = System.IO.File.OpenRead(path);
            return File(image, contentType);
        }

        [AllowAnonymous]
        [HttpGet("groupMedia/{mediaId}")]
        public IActionResult GroupMediaAsync(Guid mediaId)
        {
            GroupMediaModel<GroupMedia> groupMediaModel = new GroupMediaModel<GroupMedia>();
            GroupMedia groupMedia = groupMediaModel.GetData(GroupMediaId: mediaId).FirstOrDefault();
            if (groupMedia == null)
            {
                return NotFound();
            }
            string path = groupMedia.GroupMediaURL;
            string contentType = groupMedia.ContentType;
            var image = System.IO.File.OpenRead(path);
            return File(image, contentType);
        }

        [HttpPost("addGroupMedia")]
        [Authorize]
        public IActionResult AddGroupMedia([FromHeader(Name = "groupId")] Guid groupId, UploadedDocument uploadedDocument)
        {
            Guid currentUserId = Guid.Parse(User.Identity.GetUserId());
            if (uploadedDocument.ContentType == "mp4")
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    "No videos allowed"
                    },
                    Status = 0,
                    Result = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "No videos allowed"
                });
            }
            GroupModel<Group> groupModel = new GroupModel<Group>();
            Group group = groupModel.Get(groupId);

            if (group == null)
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    "Group not found"
                    },
                    Status = 0,
                    Result = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Group not found"
                });
            }
            string errorMessage = "";
            String fileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-ffff");
            string imagePath = UploadDocument(uploadedDocument,fileName, _configuration["GroupMediaPath"] + groupId.ToString(), ref errorMessage);
            if (imagePath == null)
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    errorMessage
                    },
                    Status = 0,
                    Result = false,
                    Message = "Could not save image"
                });
            }

            GroupMediaModel<GroupMedia> groupMediaModel = new GroupMediaModel<GroupMedia>();

            var newGroupMedia = groupMediaModel.Insert(new GroupMedia()
            {
                CreateDate = DateTime.Now,
                CreateUserId = currentUserId,
                ContentType = uploadedDocument.ContentType,
                GroupMediaURL = imagePath,
                GroupId = groupId,
                IsBlock = false,
                IsDeleted = false,
            });

            if (newGroupMedia == null)
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    "Couldn't add image"
                    },
                    Status = 0,
                    Result = false,
                    Message = "Couldn't add image"
                });
            }

            return Ok(new JsonResponse<bool>()
            {
                Errors = new List<string> {
                    "image added successfully"
                    },
                Status = 1,
                Result = true,
                Message = "image added successfully",
            });
        }

        
        [HttpPost("updateGroupImage")]
        [Authorize]
        public IActionResult UpdateGroupImage([FromHeader(Name = "groupId")] Guid groupId, UploadedDocument uploadedDocument)
        {
            GroupModel<Group> groupModel = new GroupModel<Group>();
            Group group = groupModel.Get(groupId);

            if (group == null)
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    "Group not found"
                    },
                    Status = 0,
                    Result = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "User not found"
                });
            }
            string errorMessage = "";
            string imagePath = UploadDocument(uploadedDocument, groupId.ToString(), _configuration["GroupProfilePicturePath"], ref errorMessage);
            if (imagePath == null)
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    errorMessage
                    },
                    Status = 0,
                    Result = false,
                    Message = "Could not save image"
                });
            }

            group.GroupImageURL = imagePath;
            group.ImageContentType = uploadedDocument.ContentType;

            Group updatedGroup = groupModel.Update(groupId, group);
            if(updatedGroup == null)
            {
                return BadRequest(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    "Couldn't update group"
                    },
                    Status = 0,
                    Result = false,
                    Message = "Couldn't update group"
                });
            }

            return Ok(new JsonResponse<bool>()
            {
                Status = 1,
                Result = true,
                Message = "image updated successfully",
            });
        }

        private string UploadDocument(UploadedDocument uploadedDocument, string fileName, string folderPath, ref string errorMessage)
        {
            bool isValid = Classes.Utilities.Utility.ValidateUploadedDocument(uploadedDocument.ContentType, uploadedDocument.FileSize, ref errorMessage);
            if (isValid)
            {
                string filePath = Classes.Utilities.Utility.saveFile(fileName, folderPath, uploadedDocument);
                return filePath;
            }
            return null;
        }

    }
}
