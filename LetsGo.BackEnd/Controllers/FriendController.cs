using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Common;
using DataLayer.Security.TableEntity;
using GenericBackEndCore.Classes.Utilities;
using LetsGo.BackEnd.Models;
using LetsGo.DataLayer;
using LetsGo.DataLayer.TableEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public FriendController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetFriendsAsync()
        {
            GroupModel<Group> groupModel = new GroupModel<Group>();
            List<User> users = new List<User>();
            Guid? userId = Guid.Parse(User.Identity.GetUserId());
            UserGroupModel<UserGroup> userGroupModel = new UserGroupModel<UserGroup>();
            List<UserGroup> userGroups = userGroupModel.GetData(UserId: userId).ToList();
            if (userGroups != null && userGroups.Any())
            {
                foreach (UserGroup item in userGroups)
                {
                    Group group = groupModel.Get(item.GroupId);
                    group.UserGroups = userGroupModel.GetData(GroupId: group.GroupId).ToList();
                    if (group != null && group.UserGroups != null && group.UserGroups.Any())
                    {
                        foreach (UserGroup gp in group.UserGroups)
                        {
                            var friend = await _userManager.FindByIdAsync(gp.UserId.ToString());
                            if (friend.Id != userId && !users.Contains(friend))
                            {
                                users.Add(friend);
                            }
                        }
                    }
                      
                }
            }
            List<GroupMember> friends = new List<GroupMember>();
            friends = CoreUtility.CopyObject<User, GroupMember>(users).ToList();
            return Ok(friends);

        }
    }
}
