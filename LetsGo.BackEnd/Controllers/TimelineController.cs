using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classes.Utilities;
using DataLayer.Common;
using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimelineController : ControllerBase
    {
        [HttpGet("getRoutines")]
        public IActionResult getRoutines(
            [FromQuery(Name = "fromDate")] DateTime fromDate, 
            [FromQuery(Name = "toDate")] DateTime toDate
            )
        {
            Guid? userId = Guid.Parse(User.Identity.GetUserId());
            //get user routines
            //from routine 
            IEnumerable<Routine> userRoutines = new RoutineModel<Routine>()
                .GetData(UserId: userId, IncludeReferences: "RoutineDays");

            //from user groups
            IEnumerable<Routine> userGroupRoutines = new UserGroupModel<UserGroup>()
                .GetData(UserId: userId, IncludeReferences: "Group,Group.Routine,Group.Routine.RoutineDays")
                .Select(x => x.Group.Routine).Distinct();
            List<Routine> routines = new List<Routine>();

            routines.AddRange(userRoutines);
            routines.AddRange(userGroupRoutines);
            routines = routines.GroupBy(x => x.RoutineId).Select(x => x.First()).ToList();

            //IEnumerable<IGrouping<Routine>> routineGroupedByDay = routines.GroupBy(x => x.)

            DateTime date = fromDate;
            List<TimeLineRoutineResponse> timeLineRoutineModels = new List<TimeLineRoutineResponse>();
            do
            {
                var day = date.ToString("dddd");
                var dayRoutines = routines.Where(x => x.RoutineDays.Any(y => y.Day == day));
                List<TimeLineRoutineDayResponse> timeLineRoutineDayModels = new List<TimeLineRoutineDayResponse>();
                foreach (Routine item in dayRoutines)
                {
                    IEnumerable<TimeLineRoutineDayResponse> timeLineRoutineDays = item.RoutineDays.Where(x=>x.Day == day && x.IsDeleted == false)
                        .Select(x => new TimeLineRoutineDayResponse() 
                        {
                            FromTime = x.FromTime,
                            ToTime = x.ToTime,
                            RoutineId = x.Routine.RoutineId,
                            RoutineName = x.Routine.RoutineName,
                            RoutineAltName = x.Routine.RoutineAltName
                        });
                    timeLineRoutineDayModels.AddRange(timeLineRoutineDays);
                }

                TimeLineRoutineResponse timeLineRoutineDayModel = new TimeLineRoutineResponse()
                {
                    Date = date,
                    Day = day,
                    RoutineDays = timeLineRoutineDayModels
                };
                timeLineRoutineModels.Add(timeLineRoutineDayModel);
                date = date.AddDays(1);
            } while (date <= toDate);

            return Ok(timeLineRoutineModels);
        }

    }
}
