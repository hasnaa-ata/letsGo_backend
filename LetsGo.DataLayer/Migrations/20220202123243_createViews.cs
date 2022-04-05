using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsGo.DataLayer.Migrations
{
    public partial class createViews : Migration
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

            #region View Scripts
            //Create_CountryView
            migrationBuilder.Sql("Create VIEW [CountryView] " +
               "AS " +
               "SELECT Country.CountryId, Country.CountryName, Country.CountryAltName, Country.CountryCode, Country.PhoneRegExp, Country.MobileRegExp, Country.NationalIdentityRegExp, " +
               "Country.IsBlock, (CASE WHEN Country.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
               "Country.IsDeleted, Country.CreateUserId, Country.CreateDate, Country.ModifyUserId, Country.ModifyDate, " +
               "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
               "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
               "FROM [dbo].Country AS Country INNER JOIN " +
               "[security].[UserInfoView] AS CreateUser ON Country.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
               "[security].[UserInfoView] AS ModifyUser ON Country.ModifyUserId = ModifyUser.UserId");

            //Create_StateView
            migrationBuilder.Sql("Create VIEW [StateView] " +
                "AS " +
                "SELECT State.StateId, State.StateName, State.StateAltName, Country.CountryCode, Country.CountryId, Country.CountryName, Country.CountryAltName, " +
                "State.IsBlock, (CASE WHEN State.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "State.IsDeleted, State.CreateUserId, State.CreateDate, State.ModifyUserId, State.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [dbo].State AS State INNER JOIN " +
                "[dbo].Country AS Country ON State.CountryId = Country.CountryId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON State.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON State.ModifyUserId = ModifyUser.UserId");

            //Create_CityView
            migrationBuilder.Sql("Create VIEW [CityView] " +
                "AS " +
                "SELECT City.CityId, City.CityName, City.CityAltName, City.StateId, State.StateName, State.StateAltName, " +
                "State.CountryId, Country.CountryName, Country.CountryAltName, " +
                "City.IsBlock, (CASE WHEN City.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "City.IsDeleted, City.CreateUserId, City.CreateDate, City.ModifyUserId, City.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [dbo].City AS City INNER JOIN " +
                "[dbo].State AS State ON City.StateId = State.StateId INNER JOIN " +
                "[dbo].Country AS Country ON State.CountryId = Country.CountryId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON City.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON City.ModifyUserId = ModifyUser.UserId");

            //Create_RoutineCategoryView
            migrationBuilder.Sql("Create VIEW [RoutineCategoryView] " +
                "AS " +
                "SELECT RoutineCategory.RoutineCategoryId, RoutineCategory.RoutineCategoryName, RoutineCategory.RoutineCategoryAltName, " +
                "RoutineCategory.IsBlock, (CASE WHEN RoutineCategory.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "RoutineCategory.IsDeleted, RoutineCategory.CreateUserId, RoutineCategory.CreateDate, RoutineCategory.ModifyUserId, RoutineCategory.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName,  " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [dbo].RoutineCategory AS RoutineCategory INNER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON RoutineCategory.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON RoutineCategory.ModifyUserId = ModifyUser.UserId");

            //Create_RoutineRouteTypeView
            migrationBuilder.Sql("Create VIEW [RoutineRouteTypeView] " +
                "AS " +
                "SELECT RoutineRouteType.RoutineRouteTypeId, RoutineRouteType.RoutineRouteTypeName, RoutineRouteType.RoutineRouteTypeAltName, " +
                "RoutineRouteType.IsBlock, (CASE WHEN RoutineRouteType.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "RoutineRouteType.IsDeleted, RoutineRouteType.CreateUserId, RoutineRouteType.CreateDate, RoutineRouteType.ModifyUserId, RoutineRouteType.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [dbo].RoutineRouteTypes AS RoutineRouteType INNER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON RoutineRouteType.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON RoutineRouteType.ModifyUserId = ModifyUser.UserId");

            //Create_RoutineView
            migrationBuilder.Sql("Create VIEW [RoutineView] " +
                "AS " +
                "SELECT Routine.RoutineId, Routine.RoutineName, Routine.RoutineAltName, Routine.UserId, Routine.RoutineSourceLatitude, Routine.RoutineSourceLongtitude, " +
                "Routine.RoutineDestinationAdderss, Routine.RoutineDestinationLatitude, Routine.RoutineDestinationLongtitude, Routine.RoutineSourceAdderss, " +
                "Routine.Description, Routine.AltDescription, Routine.IsBlock, (CASE WHEN Routine.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "RoutineCategory.RoutineCategoryId, RoutineCategory.RoutineCategoryName, RoutineCategory.RoutineCategoryAltName, " +
                "RoutineRouteType.RoutineRouteTypeId, RoutineRouteType.RoutineRouteTypeName, RoutineRouteType.RoutineRouteTypeAltName, " +
                "Routine.IsDeleted, Routine.CreateUserId, Routine.CreateDate, Routine.ModifyUserId, Routine.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "From [dbo].Routine AS Routine INNER JOIN[dbo].RoutineCategory AS RoutineCategory " +
                "ON Routine.RoutineCategoryId = RoutineCategory.RoutineCategoryId LEFT OUTER JOIN [dbo].RoutineRouteTypes AS RoutineRouteType " +
                "ON Routine.RoutineRouteTypeId = RoutineRouteType.RoutineRouteTypeId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON Routine.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON Routine.ModifyUserId = ModifyUser.UserId");

            //Create_GroupStatusView
            migrationBuilder.Sql("Create VIEW [GroupStatusView] " +
                "AS " +
                "SELECT GroupStatus.GroupStatusId, GroupStatus.GroupStatusName, GroupStatus.GroupStatusAltName, " +
                "GroupStatus.IsBlock, (CASE WHEN GroupStatus.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "GroupStatus.IsDeleted, GroupStatus.CreateUserId, GroupStatus.CreateDate, GroupStatus.ModifyUserId, GroupStatus.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "FROM [dbo].GroupStatus AS GroupStatus INNER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON GroupStatus.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON GroupStatus.ModifyUserId = ModifyUser.UserId");

            //Create_GroupView
            migrationBuilder.Sql("Create VIEW [GroupView] " +
               "AS " +
               "SELECT GP.GroupId, UserGroup.UserId, GP.GroupName, GP.GroupAltName, GP.GroupImageURL, GP.Description, GP.AltDescription, GP.MaxNoMembers, " +
               "GroupStatus.GroupStatusId, GroupStatus.GroupStatusName, GroupStatus.GroupStatusAltName, " +
               "Routine.RoutineId, Routine.RoutineName, Routine.RoutineAltName, " +
               "GP.IsBlock, (CASE WHEN Routine.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
               "GP.IsDeleted, GP.CreateUserId, GP.CreateDate, GP.ModifyUserId, GP.ModifyDate,  " +
               "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
               "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
               "From [dbo].[Group] AS GP INNER JOIN [dbo].Routine AS Routine " +
               "ON GP.RoutineId = Routine.RoutineId INNER JOIN [dbo].UserGroup AS UserGroup " +
               "ON GP.GroupId = UserGroup.GroupId and UserGroup.IsDeleted = 0 " +
               "LEFT OUTER JOIN [dbo].GroupStatus AS GroupStatus " +
               "ON GP.GroupStatusId = GroupStatus.GroupStatusId LEFT OUTER JOIN " +
               "[security].[UserInfoView] AS CreateUser ON GP.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
               "[security].[UserInfoView] AS ModifyUser ON GP.ModifyUserId = ModifyUser.UserId");

            //Create_UserGroupView
            migrationBuilder.Sql("Create VIEW [UserGroupView] " +
                "AS " +
                "SELECT UserGroup.UserGroupId, GP.GroupId, GP.GroupName, GP.GroupAltName, UserGroup.UserId, " +
                "UserInfo.UserFullName, UserInfo.UserAltFullName, UserRole.RoleId, UserRole.RoleName, UserRole.RoleAltName, " +
                "GP.IsBlock, (CASE WHEN UserGroup.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "GP.IsDeleted, GP.CreateUserId, GP.CreateDate, GP.ModifyUserId, GP.ModifyDate, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "From [dbo].UserGroup AS UserGroup INNER JOIN[dbo].[Group] AS GP " +
                "ON UserGroup.GroupId = GP.GroupId LEFT OUTER JOIN[security].[Role] AS UserRole " +
                "ON UserGroup.RoleId = UserRole.RoleId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS UserInfo ON UserGroup.UserId = UserInfo.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON GP.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON GP.ModifyUserId = ModifyUser.UserId");

            //Create_InvitationView
            migrationBuilder.Sql("Create VIEW [InvitationView] " +
                "AS " +
                "SELECT Invitation.InvitationId, Invitation.UserId, Invitation.InvitationStatus, GP.GroupId, GP.GroupName, GP.GroupAltName, " +
                "Invitation.IsBlock, (CASE WHEN Invitation.IsBlock = 1 THEN 'true' ELSE 'false' END) AS IsBlock_str, " +
                "Invitation.IsDeleted, GP.CreateUserId, Invitation.CreateDate, Invitation.ModifyUserId, Invitation.ModifyDate, " +
                "UserInfo.UserFullName, UserInfo.UserAltFullName, " +
                "CreateUser.UserFullName AS CreateUser_FullName, CreateUser.UserAltFullName AS CreateUser_FullAltName, " +
                "ModifyUser.UserFullName AS ModifyUser_FullName, ModifyUser.UserAltFullName AS ModifyUser_FullAltName " +
                "From [dbo].Invitation AS Invitation INNER JOIN[dbo].[Group] AS GP ON Invitation.GroupId = GP.GroupId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS UserInfo ON Invitation.UserId = UserInfo.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS CreateUser ON Invitation.CreateUserId = CreateUser.UserId LEFT OUTER JOIN " +
                "[security].[UserInfoView] AS ModifyUser ON Invitation.ModifyUserId = ModifyUser.UserId");

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

            #region View Scripts
            migrationBuilder.Sql("DROP VIEW [CountryView]");

            migrationBuilder.Sql("DROP VIEW [StateView]");

            migrationBuilder.Sql("DROP VIEW [CityView]");

            migrationBuilder.Sql("DROP VIEW [RoutineCategoryView]");

            migrationBuilder.Sql("DROP VIEW [RoutineRouteTypeView]");

            migrationBuilder.Sql("DROP VIEW [RoutineView]");

            migrationBuilder.Sql("DROP VIEW [GroupStatusView]");

            migrationBuilder.Sql("DROP VIEW [GroupView]");

            migrationBuilder.Sql("DROP VIEW [UserGroupView]");

            migrationBuilder.Sql("DROP VIEW [InvitationView]");
            #endregion
        }
    }
}
