using Classes.Utilities;
using DataLayer.Common;
using DataLayer.Security.TableEntity;
using LetsGo.DataLayer;
using Mawid.BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlusAction.BackEnd.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility = Classes.Utilities.Utility;

namespace LetsGo.BackEnd.Services
{
    public interface IUserService
    {
        Task<JsonResponse<bool>> RegisterUserAsync(RegisterViewModel model);
        Task<JsonResponse<string>> LoginUserAsync(UserLoginViewModel model);
        Task<JsonResponse<bool>> ConfirmEmailAsync(Guid userId, string token);
        User GetUser(Guid userId);
        Task<JsonResponse<bool>> UpdateProfilePictureAsync(Guid? userId, UploadedDocument uploadedDocument);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly LetsGoDBContext _dBContext;

        public UserService(UserManager<User> userManager, IConfiguration configuration, LetsGoDBContext dBContext)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._dBContext = dBContext;
        }

        public async Task<JsonResponse<bool>> RegisterUserAsync(RegisterViewModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new NullReferenceException("Register model null");
                }

                if (model.ConfirmPassword != model.Password)
                {
                    return new JsonResponse<bool>()
                    {
                        Status = 0,
                        Result = false,
                        Message = "Confirm password doesn't match with password"
                    };
                }

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserFullName = model.UserFullName,
                    UserAltFullName = model.UserAltFullName,
                    JobTitle = model.JobTitle,
                    WorkPlace = model.WorkPlace,
                    CreateDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                    var emailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    return new JsonResponse<bool>()
                    {
                        Status = 1,
                        Result = true,
                        Message = "User created successfully",
                        //RedirectTo = $"{_configuration["AppUrl"]}/api/account/confirmEmail?userId={user.Id}&token={emailToken}"
                    };
                }

                return new JsonResponse<bool>()
                {
                    Status = 0,
                    Result = false,
                    Message = "Fail to create user"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonResponse<string>> LoginUserAsync(UserLoginViewModel model)
        {
            try
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        //create token
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(IdentityCustomClaims.UserId, user.Id.ToString()),
                            new Claim(IdentityCustomClaims.UserName, user.UserName),
                            new Claim(IdentityCustomClaims.UserFullName, user.UserFullName),
                            new Claim(IdentityCustomClaims.UserAltFullName, user.UserAltFullName),
                            new Claim(IdentityCustomClaims.Email, user.Email),
                            new Claim(IdentityCustomClaims.PhoneNumber, user.PhoneNumber),
                            new Claim(IdentityCustomClaims.City, user.CityId.HasValue ? user.CityId.ToString() : ""),
                            new Claim(IdentityCustomClaims.ImageURL, user.ImageURL ?? ""),
                            new Claim(IdentityCustomClaims.JobTitle, user.JobTitle ?? ""),
                            new Claim(IdentityCustomClaims.WorkPlace, user.WorkPlace ?? ""),
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

                        var token = new JwtSecurityToken(
                            issuer: _configuration["AuthSettings:Issuer"],
                            audience: _configuration["AuthSettings:Audince"],
                            claims: claims,
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                            );

                        string token_str = new JwtSecurityTokenHandler().WriteToken(token);

                        return new JsonResponse<string>()
                        {
                            Status = 1,
                            Result = token_str,
                            Message = "Expired Date:" + token.ValidTo.ToString("yyyy-MM-dd hh:mm tt")
                        };
                    }
                }

                return new JsonResponse<string>()
                {
                    Status = 0,
                    Message = "Incorrect username or password"
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonResponse<bool>> ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new JsonResponse<bool>()
                {
                    Status = 0,
                    Result = false,
                    Message = "User not found"
                };
            }
            var decodedEmailToken = WebEncoders.Base64UrlDecode(token);
            var emailToken = Encoding.UTF8.GetString(decodedEmailToken);

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);
            if (result.Succeeded)
            {
                return new JsonResponse<bool>()
                {
                    Status = 1,
                    Result = true,
                    Message = "Email Confirmed Successfully"
                };
            }

            return new JsonResponse<bool>()
            {
                Status = 0,
                Result = false,
                Message = "Email did not confirmed",
            };
        }

        public async Task<JsonResponse<bool>> UpdateProfilePictureAsync(Guid? userId, UploadedDocument uploadedDocument)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    "User not found"
                    },
                    Status = 0,
                    Result = false,
                    Message = "User not found"
                };
            }
            string errorMessage = "";
            string imagePath = UploadDocument(uploadedDocument, userId.ToString(), ref errorMessage);
            if(imagePath == null)
            {
                return new JsonResponse<bool>()
                {
                    Errors = new List<string> {
                    errorMessage
                    },
                    Status = 0,
                    Result = false,
                    Message = "Could not save image"
                };
            }

            user.ImageURL = imagePath;
            user.ImageContentType = uploadedDocument.ContentType;
            _dBContext.SaveChanges();
            return new JsonResponse<bool>()
            {
                Errors = new List<string> {
                    "image updated successfully"
                    },
                Status = 1,
                Result = true,
                Message = "image updated successfully",
            };
        }



        private string UploadDocument(UploadedDocument uploadedDocument, string fileName, ref string errorMessage)
        {
            bool isValid = Utility.ValidateUploadedDocument(uploadedDocument.ContentType, uploadedDocument.FileSize, ref errorMessage);
            if (isValid)
            {
                string filePath = Utility.saveFile(fileName, "App_Data/Images/ProfilePictures", uploadedDocument);
                return filePath;
            }
            return null;
        }

        public User GetUser(Guid userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString());
            return user.Result;
        }
    }
}
