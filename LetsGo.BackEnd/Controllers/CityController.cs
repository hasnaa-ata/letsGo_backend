using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using Mawid.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseApiController<City, CityView, CityIndexViewModel, CityDetailsViewModel,
     CityCreateBindModel, CityEditBindModel, CityCreateModel, CityEditModel, City,
     CityModel<City>, CityModel<CityView>>
    {
    }
}
