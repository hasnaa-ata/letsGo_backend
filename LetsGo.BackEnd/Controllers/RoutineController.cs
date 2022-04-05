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
using static GenericBackEndCore.Classes.Common.Enums;

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
            _userManager = userManager;
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
        
        public override IActionResult FuncPostEdit(bool success, ref RoutineEditBindModel model, ref Routine updatedItem, Guid? notificationId, Guid? taskId, ref JsonResponse<Routine> response)
        {
            return base.FuncPostEdit(success, ref model, ref updatedItem, notificationId, taskId, ref response);
        }

        //public override IActionResult Edit([FromRoute] Guid id, [FromBody] RoutineEditBindModel model, [FromRoute] Guid? notificationId, [FromRoute] Guid? taskId)
        //{
        //    JsonResponse<Routine> response = new JsonResponse<Routine>();
        //    GrantPermission = UserHasPermission(AccessType.Edit);
        //    if (GrantPermission)
        //    {
        //        List<RoutineDay> addedRoutineDays = new List<RoutineDay>();
        //        List<RoutineDay> deletedRoutineDays = new List<RoutineDay>();
        //        List<RoutineDay> editedRoutineDays = new List<RoutineDay>();
        //        //get new
        //        addedRoutineDays = model.RoutineDays.Where(x => x.RoutineDayId == null || x.RoutineDayId == Guid.Empty).ToList();

        //        List<RoutineDay> oldRoutineDays = new RoutineDayModel<RoutineDay>().GetData(RoutineId: id).ToList();
        //        if (ModelState.IsValid)
        //        {
        //            DelegatePreEdit delegatePreExecute = new DelegatePreEdit(FuncPreEdit);
        //            //select routine with days
        //            if (delegatePreExecute(id, ref model, ref response))
        //            {

        //                //create instance to update object
        //                RoutineModel<Routine> instance = new RoutineModel<Routine>();
        //                Routine updatedItem = null;
        //                if (IsEdit_WithReference)
        //                {
        //                    updatedItem = instance.Update_WithReference(id, model, EntityReferences);
        //                }
        //                else
        //                {
        //                    updatedItem = instance.Update(id, model);
        //                }
        //                bool success = (updatedItem != null);

        //                if (success)
        //                {
        //                    //get deleted alarms
        //                    deletedRoutineDays = oldRoutineDays.Where(x => updatedItem.RoutineDays.Any(y => y.RoutineDayId == x.RoutineDayId && y.IsDeleted)).ToList();
        //                    //deletedRoutineDays = oldRoutineDays.Where(x => !updatedItem.RoutineDays.Any(y => y.RoutineDayId == x.RoutineDayId)).ToList();
        //                    //get uppdated
        //                    editedRoutineDays = updatedItem.RoutineDays.Where(x => oldRoutineDays.Any(y => y.RoutineDayId == x.RoutineDayId && y.FromTime != x.FromTime)).ToList();
        //                }

        //                DelegatePostEdit delegatePostExecute = new DelegatePostEdit(FuncPostEdit);
        //                delegatePostExecute(success, ref model, ref updatedItem, notificationId, taskId, ref response);
        //                if (success)
        //                {
        //                    RoutineAlarm routineAlarm = new RoutineAlarm()
        //                    {
        //                        AlarmsToAdd = addedRoutineDays,
        //                        AlarmsToDelete = deletedRoutineDays,
        //                        AlarmsToEdit = editedRoutineDays,
        //                    };
        //                    JsonResponse<RoutineAlarm> newResponse = new JsonResponse<RoutineAlarm>()
        //                    {
        //                        Errors = response.Errors,
        //                        HttpStatusCode = response.HttpStatusCode,
        //                        Message = response.Message,
        //                        RedirectTo = response.RedirectTo,
        //                        Result = routineAlarm,
        //                        Status = response.Status
        //                    };

        //                    return Ok(newResponse);
        //                }
        //            }
        //        }
        //    }
        //    return GetUnAuthorized();
        //}
        public override bool FuncPreEdit(Guid id, ref RoutineEditBindModel model, ref JsonResponse<Routine> response)
        {
            IsEdit_WithReference = true;
            EntityReferences = "RoutineDays";
            RoutineModel<Routine> routineModel = new RoutineModel<Routine>();
            IEnumerable<Routine> oldModels = routineModel.GetData(RoutineId: id, IncludeReferences: "RoutineDays");
            if (oldModels != null && oldModels.Any())
            {
                //get Routine old status
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
                        foreach (RoutineDay routineDay in model.RoutineDays.Where(x => oldModel.RoutineDays
                        .Any(y => y.RoutineDayId.ToString() == x.RoutineDayId.ToString())))
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

        //public override IActionResult FuncPostDelete(bool success, Guid id, Guid? notificationId, Guid? taskId, ref JsonResponse<bool> response)
        //{
        //    if (success)
        //    {
        //        //get routine days
        //        List<RoutineDay> routineDays = new RoutineDayModel<RoutineDay>().GetData(RoutineId: id).ToList();
        //        JsonResponse<List<RoutineDay>> newResponse = new JsonResponse<List<RoutineDay>>() 
        //        {
        //            Errors = response.Errors,
        //            HttpStatusCode = response.HttpStatusCode,
        //            Message = response.Message,
        //            RedirectTo = response.RedirectTo,
        //            Result = routineDays,
        //            Status = response.Status
        //        };

        //        return FuncPostAction<List<RoutineDay>>(success, Transactions.Delete, 1, notificationId, taskId, ref newResponse, routineDays);
        //    }
        //    else
        //    {
        //        response.Message = MessageResource.ErrorMsg_ItemNotFound;
        //        return NotFound(response);
        //    }
        //}
    }
}
