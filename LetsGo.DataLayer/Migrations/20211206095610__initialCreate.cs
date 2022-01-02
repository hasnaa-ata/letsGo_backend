using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsGo.DataLayer.Migrations
{
    public partial class _initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "AccessType",
                schema: "security",
                columns: table => new
                {
                    AccessTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccessTypeAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessType", x => x.AccessTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PhoneRegExp = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MobileRegExp = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NationalIdentityRegExp = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "GroupStatus",
                columns: table => new
                {
                    GroupStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupStatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupStatusAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStatus", x => x.GroupStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "security",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoutineCategory",
                columns: table => new
                {
                    RoutineCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoutineCategoryAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineCategory", x => x.RoutineCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RoutineRouteTypes",
                columns: table => new
                {
                    RoutineRouteTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineRouteTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoutineRouteTypeAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineRouteTypes", x => x.RoutineRouteTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "security",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceAltName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServiceDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ServiceAltDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "security",
                columns: table => new
                {
                    RoleClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.RoleClaimId);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleService",
                schema: "security",
                columns: table => new
                {
                    RoleServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleService", x => x.RoleServiceId);
                    table.ForeignKey(
                        name: "FK_RoleService_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "security",
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAccess",
                schema: "security",
                columns: table => new
                {
                    ServiceAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAccess", x => x.ServiceAccessId);
                    table.ForeignKey(
                        name: "FK_ServiceAccess_AccessType_AccessTypeId",
                        column: x => x.AccessTypeId,
                        principalSchema: "security",
                        principalTable: "AccessType",
                        principalColumn: "AccessTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceAccess_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "security",
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityAltName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleServiceAccess",
                schema: "security",
                columns: table => new
                {
                    RoleServiceAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleServiceAccess", x => x.RoleServiceAccessId);
                    table.ForeignKey(
                        name: "FK_RoleServiceAccess_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleServiceAccess_ServiceAccess_ServiceAccessId",
                        column: x => x.ServiceAccessId,
                        principalSchema: "security",
                        principalTable: "ServiceAccess",
                        principalColumn: "ServiceAccessId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserFullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserAltFullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routine",
                columns: table => new
                {
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoutineName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RoutineAltName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RoutineCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoutineRouteTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AltDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RoutineSourceLatitude = table.Column<double>(type: "float", nullable: false),
                    RoutineSourceLongtitude = table.Column<double>(type: "float", nullable: false),
                    RoutineDestinationLatitude = table.Column<double>(type: "float", nullable: false),
                    RoutineDestinationLongtitude = table.Column<double>(type: "float", nullable: false),
                    RoutineSourceAdderss = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RoutineDestinationAdderss = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routine", x => x.RoutineId);
                    table.ForeignKey(
                        name: "FK_Routine_RoutineCategory_RoutineCategoryId",
                        column: x => x.RoutineCategoryId,
                        principalTable: "RoutineCategory",
                        principalColumn: "RoutineCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routine_RoutineRouteTypes_RoutineRouteTypeId",
                        column: x => x.RoutineRouteTypeId,
                        principalTable: "RoutineRouteTypes",
                        principalColumn: "RoutineRouteTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routine_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "security",
                columns: table => new
                {
                    UserClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.UserClaimId);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserService",
                schema: "security",
                columns: table => new
                {
                    UserServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserService", x => x.UserServiceId);
                    table.ForeignKey(
                        name: "FK_UserService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "security",
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserService_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserServiceAccess",
                schema: "security",
                columns: table => new
                {
                    UserServiceAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceAccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserServiceAccess", x => x.UserServiceAccessId);
                    table.ForeignKey(
                        name: "FK_UserServiceAccess_ServiceAccess_ServiceAccessId",
                        column: x => x.ServiceAccessId,
                        principalSchema: "security",
                        principalTable: "ServiceAccess",
                        principalColumn: "ServiceAccessId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserServiceAccess_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GroupAltName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    AltDescription = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    GroupStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxNoMembers = table.Column<int>(type: "int", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_GroupStatus_GroupStatusId",
                        column: x => x.GroupStatusId,
                        principalTable: "GroupStatus",
                        principalColumn: "GroupStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Group_Routine_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routine",
                        principalColumn: "RoutineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoutineDay",
                columns: table => new
                {
                    RoutineDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    RoutineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineDay", x => x.RoutineDayId);
                    table.ForeignKey(
                        name: "FK_RoutineDay_Routine_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "Routine",
                        principalColumn: "RoutineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    InvitationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.InvitationId);
                    table.ForeignKey(
                        name: "FK_Invitation_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitation_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UserGroupId);
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroup_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessType_AccessTypeAltName",
                schema: "security",
                table: "AccessType",
                column: "AccessTypeAltName",
                unique: true,
                filter: "[AccessTypeAltName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccessType_AccessTypeName",
                schema: "security",
                table: "AccessType",
                column: "AccessTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupStatusId",
                table: "Group",
                column: "GroupStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_RoutineId",
                table: "Group",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStatus_GroupStatusAltName",
                table: "GroupStatus",
                column: "GroupStatusAltName",
                unique: true,
                filter: "[GroupStatusAltName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStatus_GroupStatusName",
                table: "GroupStatus",
                column: "GroupStatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_GroupId",
                table: "Invitation",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserId",
                table: "Invitation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "security",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleService_RoleId",
                schema: "security",
                table: "RoleService",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleService_ServiceId",
                schema: "security",
                table: "RoleService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleServiceAccess_RoleId",
                schema: "security",
                table: "RoleServiceAccess",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleServiceAccess_ServiceAccessId",
                schema: "security",
                table: "RoleServiceAccess",
                column: "ServiceAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_Routine_RoutineCategoryId",
                table: "Routine",
                column: "RoutineCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Routine_RoutineRouteTypeId",
                table: "Routine",
                column: "RoutineRouteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Routine_UserId",
                table: "Routine",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineCategory_RoutineCategoryAltName",
                table: "RoutineCategory",
                column: "RoutineCategoryAltName",
                unique: true,
                filter: "[RoutineCategoryAltName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineCategory_RoutineCategoryName",
                table: "RoutineCategory",
                column: "RoutineCategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDay_RoutineId",
                table: "RoutineDay",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineRouteTypes_RoutineRouteTypeAltName",
                table: "RoutineRouteTypes",
                column: "RoutineRouteTypeAltName",
                unique: true,
                filter: "[RoutineRouteTypeAltName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineRouteTypes_RoutineRouteTypeName",
                table: "RoutineRouteTypes",
                column: "RoutineRouteTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAccess_AccessTypeId",
                schema: "security",
                table: "ServiceAccess",
                column: "AccessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAccess_ServiceId",
                schema: "security",
                table: "ServiceAccess",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CityId",
                schema: "security",
                table: "User",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "security",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_RoleId",
                table: "UserGroup",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                table: "UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "security",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "security",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserService_ServiceId",
                schema: "security",
                table: "UserService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserService_UserId",
                schema: "security",
                table: "UserService",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceAccess_ServiceAccessId",
                schema: "security",
                table: "UserServiceAccess",
                column: "ServiceAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServiceAccess_UserId",
                schema: "security",
                table: "UserServiceAccess",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "security");

            migrationBuilder.DropTable(
                name: "RoleService",
                schema: "security");

            migrationBuilder.DropTable(
                name: "RoleServiceAccess",
                schema: "security");

            migrationBuilder.DropTable(
                name: "RoutineDay");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserService",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserServiceAccess",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "security");

            migrationBuilder.DropTable(
                name: "ServiceAccess",
                schema: "security");

            migrationBuilder.DropTable(
                name: "GroupStatus");

            migrationBuilder.DropTable(
                name: "Routine");

            migrationBuilder.DropTable(
                name: "AccessType",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "security");

            migrationBuilder.DropTable(
                name: "RoutineCategory");

            migrationBuilder.DropTable(
                name: "RoutineRouteTypes");

            migrationBuilder.DropTable(
                name: "User",
                schema: "security");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
