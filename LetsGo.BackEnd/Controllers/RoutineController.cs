using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using GenericBackEndCore.Classes.Utilities;
using DataLayer.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using GenericRepositoryCore.Utilities;
using Microsoft.AspNetCore.Identity;
using DataLayer.Security.TableEntity;
using Microsoft.AspNetCore.Authorization;

namespace LetsGo.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutineController : BaseApiController<Routine, RoutineView, RoutineIndexViewModel, RoutineDetailsViewModel,
        RoutineCreateBindModel, RoutineEditBindModel, RoutineCreateModel, RoutineEditModel, Routine,
        RoutineModel<Routine>, RoutineModel<RoutineView>>
    {

        private readonly UserManager<User> _userManager;

        public RoutineController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        public override bool FuncPreGetGridView(ref GenericDataFormat options, ref JsonResponse<PaginationResult<object>> response)
        {
            options.Filters.Add(new GenericDataFormat.FilterItem() {
            LogicalOperation = GenericDataFormat.LogicalOperation.And,
            Operation = GenericDataFormat.FilterOperation.Equal,
            Property = "UserId",
            Value = Guid.Parse(User.Identity.GetUserId())
        });
            return base.FuncPreGetGridView(ref options, ref response);
        }

        public override bool FuncPreCreate(ref RoutineCreateBindModel model, ref JsonResponse<Routine> response)
        {
            Guid? userId = Guid.Parse(User.Identity.GetUserId());
            if(userId == null)
            {
                return false;
            }
            model.UserId = userId.Value;
            model.RoutineDays = model.RoutineDays.Select(x =>
            {
                x.CreateDate = DateTime.Now;
                x.CreateUserId = userId.Value;
                return x;
            }).ToList();
            return base.FuncPreCreate(ref model, ref response);
        }

        public override IActionResult FuncPostInitEditView(Guid id, ref RoutineEditModel model, Guid? notificationId, Guid? taskId, ref JsonResponse<RoutineEditModel> response)
        {
            if (model.Item != null)
            {
                model.Item.RoutineDays = new RoutineDayModel<RoutineDay>().GetData(RoutineId: model.Item.RoutineId).ToList();
            }
            return base.FuncPostInitEditView(id, ref model, notificationId, taskId, ref response);
        }

        public override bool FuncPreEdit(Guid id, ref RoutineEditBindModel model, ref JsonResponse<Routine> response)
        {
            IsEdit_WithReference = true;
            EntityReferences = "RoutineDays";
            RoutineModel<Routine> routineModel = new RoutineModel<Routine>();
            IEnumerable<Routine> oldModels = routineModel.GetData(RoutineId: id, IncludeReferences: "RoutineDays");
            if (oldModels != null && oldModels.Any())
            {
                //get customer branch old status
                Routine oldModel = oldModels.SingleOrDefault();
                if (oldModel != null)
                {
                    if (model.RoutineDays != null && model.RoutineDays.Any())
                    {
                        foreach (RoutineDay routineDay in model.RoutineDays.Where(x => x.RoutineDayId == Guid.Empty))
                        {
                            routineDay.RoutineDayId = Guid.NewGuid();
                            routineDay.CreateDate = DateTime.Now;
                            routineDay.RoutineId = model.RoutineId;
                            routineDay.CreateUserId = Guid.Parse(User.Identity.GetUserId());
                        }
                    }

                    if (model.RoutineDays != null && model.RoutineDays.Any() &&
                        oldModel.RoutineDays != null && oldModel.RoutineDays.Any())
                    {
                        //set create user, create date for new items
                        foreach (RoutineDay routineDay in model.RoutineDays.Where(x => x.RoutineDayId == Guid.Empty))
                        {
                            routineDay.RoutineDayId = Guid.NewGuid();
                            routineDay.CreateDate = DateTime.Now;
                            routineDay.RoutineId = model.RoutineId;
                            routineDay.CreateUserId = Guid.Parse(User.Identity.GetUserId());
                        }
                        //loop over items that exist in old model items (search by id)
                        foreach (RoutineDay routineDay in model.RoutineDays.Where(x => oldModel.RoutineDays.Any(y =>
                      y.RoutineDayId.ToString() == x.RoutineDayId.ToString())))
                        {
                            //get old item
                            var oldRoutineDay = oldModel.RoutineDays.SingleOrDefault(x =>
                            x.RoutineDayId.ToString() == routineDay.RoutineDayId.ToString());
                            //check item if modified from latest time
                            if (Repository<RoutineDay>.IsChanged(
                                oldRoutineDay, routineDay, routineModel.dbContext, GenericRepositoryCoreConstant.UpdateReference_ExcludedProperties))
                            {
                                //set modify user, modify date for updated items
                                routineDay.ModifyDate = DateTime.Now;
                                routineDay.ModifyUserId = Guid.Parse(User.Identity.GetUserId());
                            }
                        }
                    }
                    routineModel.dbContext.Dispose();

                    return base.FuncPreEdit(id, ref model, ref response);
                }
            }
            return false;
        }

        public override IActionResult FuncPostDetailsView(bool success, Guid id, ref RoutineDetailsViewModel model, Guid? notificationId, ref JsonResponse<RoutineDetailsViewModel> response)
        {
            if (success)
            {
                model.RoutineDays = new RoutineDayModel<RoutineDay>().GetData(RoutineId: model.RoutineId).ToList();
            }
            return base.FuncPostDetailsView(success, id, ref model, notificationId, ref response);
        }

        [HttpGet("timelineDetail/{routineId}")]
        [Authorize]
        public IActionResult GetTimelineRoutineDetails(Guid routineId)
        {
            RoutineModel<Routine> routineModel = new RoutineModel<Routine>();
            Routine routine = routineModel.GetData(RoutineId: routineId, IncludeReferences: "Groups").FirstOrDefault();

            if(routine != null)
            {
                TimeLineRoutineDetailResponse timeLineRoutineDetailResponse = new TimeLineRoutineDetailResponse()
                {
                    Routine = routine
                };
                if (routine.Groups != null && routine.Groups.Any())
                {
                    var routineGroup = routine.Groups.FirstOrDefault();
                    Guid? currentUserId = Guid.Parse(User.Identity.GetUserId());
                    List<UserGroup> userGroups = new UserGroupModel<UserGroup>()
                        .GetData(GroupId: routineGroup.GroupId).ToList();
                    List<User> users = new List<User>();
                    if (userGroups != null && userGroups.Any())
                    {
                        foreach (UserGroup gp in userGroups)
                        {
                            var friend = _userManager.FindByIdAsync(gp.UserId.ToString()).Result;
                            users.Add(friend);
                        }
                    }
                    users = users.Where(x => x.Id != currentUserId)
                        .GroupBy(x => x.Id).Select(x => x.First()).ToList();
                    List<GroupMember> members = CoreUtility.CopyObject<User, GroupMember>(users).ToList();

                    timeLineRoutineDetailResponse.Group = routineGroup;
                    timeLineRoutineDetailResponse.IsGroupRoutine = true;
                    timeLineRoutineDetailResponse.GroupMembers = members;

                }
                timeLineRoutineDetailResponse.Routine.Groups = null;
                timeLineRoutineDetailResponse.Group.Routine = null;

                return Ok(new JsonResponse<TimeLineRoutineDetailResponse>()
                {
                    Status = 1,
                    Result = timeLineRoutineDetailResponse,
                    Message = "success"
                });
            }
            else
            {
                return NotFound(new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                   "Could not find routine"
                    },
                    Status = 0,
                    Result = false,
                    Message = "Could not find routine"
                });
            }

        }
    }
}
