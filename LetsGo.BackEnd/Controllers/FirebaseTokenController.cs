using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Common;
using GenericBackEndCore.Classes.Utilities;
using LetsGo.BackEnd.Models;
using LetsGo.DataLayer.TableEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseTokenController : BaseApiController<FirebaseToken, FirebaseToken, FirebaseToken,
        FirebaseToken, FirebaseToken, FirebaseToken, FirebaseToken, FirebaseToken, FirebaseToken,
        FirebaseTokenModel<FirebaseToken>, FirebaseTokenModel<FirebaseToken>>
    {
        public override bool FuncPreCreate(ref FirebaseToken model, ref JsonResponse<FirebaseToken> response)
        {
            Guid? userId = Guid.Parse(User.Identity.GetUserId());
            if (userId == null)
            {
                return false;
            }
            model.UserId = userId.Value;
            return base.FuncPreCreate(ref model, ref response);
        }

        public override IActionResult Create([FromBody] FirebaseToken item, [FromRoute] Guid? notificationId, [FromRoute] Guid? taskId)
        {
            FirebaseToken firebaseToken = new FirebaseTokenModel<FirebaseToken>().GetData(Token: item.Token).FirstOrDefault();
            if (firebaseToken == null)
            {
                Guid? userId = Guid.Parse(User.Identity.GetUserId());
                if (userId == null)
                {
                    return Unauthorized();
                }
                item.UserId = userId.Value;
                return base.Create(item, notificationId, taskId);
            }
            else
            {
                return Ok("Token already exists");
            }
        }



        [HttpDelete("deleteToken")]
        public IActionResult DeleteFirebaseToken([FromHeader(Name = "token")] string token)
        {
            FirebaseToken firebaseToken = new FirebaseTokenModel<FirebaseToken>().GetData(Token: token).FirstOrDefault();
            if(firebaseToken == null)
            {
                return Ok();
            }
            return base.DeleteForever(firebaseToken.FirebaseTokenId, null, null);
        }

    }

}
