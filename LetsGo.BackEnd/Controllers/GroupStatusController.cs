using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupStatusController : BaseApiController<GroupStatus, GroupStatusView, GroupStatusIndexViewModel, GroupStatusDetailsViewModel,
        GroupStatusCreateBindModel, GroupStatusEditBindModel, GroupStatusCreateModel, GroupStatusEditModel, GroupStatus,
        GroupStatusModel<GroupStatus>, GroupStatusModel<GroupStatusView>>
    {
    }
}
