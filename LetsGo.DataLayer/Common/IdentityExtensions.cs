using System;
using System.Security.Claims;
using System.Security.Principal;

namespace DataLayer.Common
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this IIdentity identity)
        {
            //var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier);
            //// Test for null to avoid issues during local testing
            //return (claim != null) ? claim.Value : string.Empty;
            return GetCustomClaimString(identity, ClaimTypes.NameIdentifier);
        }

        public static string GetUserFullName(this IIdentity identity)
        {
            return GetCustomClaimString(identity, IdentityCustomClaims.UserFullName);
        }


        public static string GetUserAltFullName(this IIdentity identity)
        {
            return GetCustomClaimString(identity, IdentityCustomClaims.UserFullName);
        }

        public static string GetUserType(this IIdentity identity)
        {
            return GetCustomClaimString(identity, IdentityCustomClaims.UserType);
        }

        public static string GetEmail(this IIdentity identity)
        {
            return GetCustomClaimString(identity, IdentityCustomClaims.Email);
        }

        public static string GetPhoneNumber(this IIdentity identity)
        {
            return GetCustomClaimString(identity, IdentityCustomClaims.PhoneNumber);
        }

        public static string GetImageURL(this IIdentity identity)
        {
            return GetCustomClaimString(identity, IdentityCustomClaims.ImageURL);
        }

        public static Guid? GetCustomer(this IIdentity identity)
        {
            return GetCustomClaimGuid(identity, IdentityCustomClaims.Customer);
        }

        public static Guid? GetCity(this IIdentity identity)
        {
            return GetCustomClaimGuid(identity, IdentityCustomClaims.City);
        }

        private static string GetCustomClaimString(IIdentity identity, string claimType)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static Guid? GetCustomClaimGuid(IIdentity identity, string claimType)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(claimType);
            Guid claimValue;
            if (Guid.TryParse(claim?.Value, out claimValue))
                return claimValue;
            return null;
        }
    }

    public static class IdentityCustomClaims
    {
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string UserFullName = "UserFullName";
        public const string UserAltFullName = "UserAltFullName";
        public const string UserType = "UserType";
        public const string Email = "Email";
        public const string PhoneNumber = "PhoneNumber";
        public const string ImageURL = "ImageURL";
        public const string Customer = "Customer";
        public const string City = "City";
        public const string JobTitle = "JobTitle";
        public const string WorkPlace = "WorkPlace";
    }

    public static class RoleIds
    {
        public static Guid User = new Guid("7010CAA3-9DF0-4A16-B63A-B98BDCD1BDFE");
        public static Guid GroupAdmin = new Guid("8DC868CD-73C8-4CC3-A8C7-13AE70899E58");
        public static Guid SuperAdministrator = new Guid("8A222BBC-3FF3-4408-B21F-65E6F463C16B");
    }
}
