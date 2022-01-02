using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using Mawid.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : BaseApiController<Country, CountryView, CountryIndexViewModel, CountryDetailsViewModel,
        CountryCreateBindModel, CountryEditBindModel, CountryCreateModel, CountryEditModel, Country,
        CountryModel<Country>, CountryModel<CountryView>>
    {
    }
}
