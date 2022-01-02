using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mawid.DataLayer.ViewBuilder
{
    internal abstract class SecurityView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region Security View Scripts

            //Create_UserInfoView
            migrationBuilder.Sql("Create VIEW [security].[UserInfoView] " +
                "AS " +
                "SELECT [User].UserId, [User].UserFullName, [User].UserAltFullName " +
                "FROM [security].[User] AS [User]");

            //Create_UserView
            migrationBuilder.Sql("Create VIEW [UserView] " +
                "AS " +
                "SELECT [User].UserId, [User].UserName, [User].UserFullName, [User].UserAltFullName, " +
                "[User].ImageURL, [User].Email, [User].PhoneNumber, [User].JobTitle, [User].WorkPlace, " +
                "[User].CityId, City.CityName, City.CityAltName, " +
                "[User].IsBlock, (CASE WHEN [User].IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "[User].IsDeleted, [User].CreateUserId, [User].CreateDate, [User].ModifyUserId, [User].ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [security].[User] AS [User] INNER JOIN " +
                "[dbo].City AS City ON [User].CityId = City.CityId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON [User].CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON [User].ModifyUserId = ModifyUser.UserId ");


            //Create_ServiceAccessView
            migrationBuilder.Sql("Create VIEW [security].[ServiceAccessView] " +
                "AS " +
                "SELECT ServiceAccess.ServiceAccessId, ServiceAccess.ServiceId, [Service].ServiceTag, [Service].ServiceName, " +
                "[Service].ServiceAltName, ServiceAccess.AccessTypeId, AccessType.AccessTypeName, AccessType.AccessTypeAltName, ServiceAccess.IsBlock " +
                "FROM [security].ServiceAccess AS ServiceAccess INNER JOIN " +
                "[security].[Service] AS[Service] ON ServiceAccess.ServiceId = [Service].ServiceId INNER JOIN " +
                "[security].AccessType AS AccessType ON ServiceAccess.AccessTypeId = AccessType.AccessTypeId"
                );

            //Create_UserRoleServiceAccessView
            migrationBuilder.Sql("Create VIEW [security].[UserRoleServiceAccessView] " +
                "AS " +
                "SELECT UserServiceAccess.UserId, NULL AS RoleId, [Service].ServiceId, [Service].ServiceTag, [Service].ServiceName, " +
                "[Service].ServiceAltName, ServiceAccess.ServiceAccessId, ServiceAccess.AccessTypeId, " +
                "UserServiceAccess.IsDeleted, UserServiceAccess.IsBlock " +
                "FROM [security].UserServiceAccess AS UserServiceAccess INNER JOIN " +
                "[security].ServiceAccess AS ServiceAccess ON UserServiceAccess.ServiceAccessId = ServiceAccess.ServiceAccessId INNER JOIN " +
                "[security].[Service] AS[Service] ON ServiceAccess.ServiceId = [Service].ServiceId " +
                "UNION " +
                "SELECT UserRole.UserId, UserRole.RoleId, [Service].ServiceId, [Service].ServiceTag, [Service].ServiceName, " +
                "[Service].ServiceAltName, ServiceAccess.ServiceAccessId, ServiceAccess.AccessTypeId, " +
                "RoleServiceAccess.IsDeleted, RoleServiceAccess.IsBlock " +
                "FROM [security].RoleServiceAccess AS RoleServiceAccess INNER JOIN " +
                "[security].UserRole AS UserRole ON RoleServiceAccess.RoleId = UserRole.RoleId INNER JOIN " +
                "[security].ServiceAccess AS ServiceAccess ON RoleServiceAccess.ServiceAccessId = ServiceAccess.ServiceAccessId INNER JOIN " +
                "[security].[Service] AS[Service] ON ServiceAccess.ServiceId = [Service].ServiceId");

            //Create_UserRoleView
            migrationBuilder.Sql("Create VIEW [security].[UserRoleView] " +
                "AS " +
                "SELECT UserRole.UserId, UserRole.RoleId, [Role].RoleName, [Role].RoleAltName, UserRole.IsDeleted, " +
                "UserRole.IsBlock, (CASE WHEN UserRole.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, UserRole.CreateUserId, " +
                "UserRole.CreateDate, CreateUser.UserFullName AS CreateUser_FullName, " +
                "CreateUser.UserAltFullName AS CreateUser_FullAltName " +
                "FROM [security].UserRole As UserRole INNER JOIN " +
                "[security].[Role] AS[Role] ON UserRole.RoleId = [Role].RoleId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON UserRole.CreateUserId = CreateUser.UserId");

            //Create_RoleServiceAccessView
            migrationBuilder.Sql("Create VIEW [security].[RoleServiceAccessView] " +
                "AS " +
                "SELECT RoleServiceAccess.RoleServiceAccessId, RoleServiceAccess.RoleId, RoleServiceAccess.ServiceAccessId, ServiceAccess.ServiceId, ServiceAccess.AccessTypeId, " +
                "RoleServiceAccess.IsBlock, (CASE WHEN RoleServiceAccess.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "RoleServiceAccess.IsDeleted, RoleServiceAccess.CreateUserId, RoleServiceAccess.CreateDate, RoleServiceAccess.ModifyUserId, RoleServiceAccess.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [security].[RoleServiceAccess] AS RoleServiceAccess INNER JOIN " +
                "[security].[ServiceAccess] AS ServiceAccess ON RoleServiceAccess.ServiceAccessId = ServiceAccess.ServiceAccessId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON RoleServiceAccess.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON RoleServiceAccess.ModifyUserId = ModifyUser.UserId");

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            #region Security View Scripts

            migrationBuilder.Sql("DROP VIEW [UserInfoView]");

            migrationBuilder.Sql("DROP VIEW [UserView]");

            migrationBuilder.Sql("DROP VIEW [security].[ServiceAccessView]");

            migrationBuilder.Sql("DROP VIEW [security].[UserRoleServiceAccessView]");

            migrationBuilder.Sql("DROP VIEW [security].[UserRoleView]");

            migrationBuilder.Sql("DROP VIEW [security].[RoleServiceAccessView]");

            #endregion
        }
    }
}
