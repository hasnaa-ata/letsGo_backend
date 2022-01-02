using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using Mawid.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : BaseApiController<State, StateView, StateIndexViewModel, StateDetailsViewModel,
       StateCreateBindModel, StateEditBindModel, StateCreateModel, StateEditModel, State,
       StateModel<State>, StateModel<StateView>>
    {

    }
}
