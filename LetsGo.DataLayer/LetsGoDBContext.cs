using DataLayer.Security.TableEntity;
using DataLayer.Security.ViewEntity;
using LetsGo.DataLayer.TableEntity;
using LetsGo.DataLayer.ViewEntity;
using Mawid.DataLayer.ViewEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace LetsGo.DataLayer
{
    public class LetsGoDBContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public LetsGoDBContext()
        {

        }

        public LetsGoDBContext(DbContextOptions<LetsGoDBContext> options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        var connectionString = configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlServer(@"Server=.;Database=LetsGo;User Id=sa;Password=Hasnaa01012175563;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetEntityIndex(ref modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            SetSecurityEntity(ref modelBuilder);
            SetViewEntity(ref modelBuilder);
        }



        private void SetEntityIndex(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoutineCategory>().HasIndex(e => new { e.RoutineCategoryName }).IsUnique();
            modelBuilder.Entity<RoutineCategory>().HasIndex(e => new { e.RoutineCategoryAltName }).IsUnique();
            modelBuilder.Entity<RoutineRouteType>().HasIndex(e => new { e.RoutineRouteTypeName }).IsUnique();
            modelBuilder.Entity<RoutineRouteType>().HasIndex(e => new { e.RoutineRouteTypeAltName }).IsUnique();
            modelBuilder.Entity<GroupStatus>().HasIndex(e => new { e.GroupStatusName }).IsUnique();
            modelBuilder.Entity<GroupStatus>().HasIndex(e => new { e.GroupStatusAltName }).IsUnique();
            modelBuilder.Entity<AccessType>().HasIndex(e => new { e.AccessTypeName }).IsUnique();
            modelBuilder.Entity<AccessType>().HasIndex(e => new { e.AccessTypeAltName }).IsUnique();
        }


        private void SetSecurityEntity(ref ModelBuilder modelBuilder)
        {
            #region Security Tables
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User", schema: "security");
                entity.Property(x => x.Id).HasColumnName("UserId");
                entity.Property(x => x.PhoneNumberConfirmed).HasDefaultValue(false);
                entity.Property(x => x.TwoFactorEnabled).HasDefaultValue(false);
                entity.Property(x => x.LockoutEnabled).HasDefaultValue(false);
                entity.Property(x => x.AccessFailedCount).HasDefaultValue(0);
                entity.Property(x => x.EmailConfirmed).HasDefaultValue(false);

                entity.Ignore(x => x.NormalizedUserName);
                entity.Ignore(x => x.NormalizedEmail);

                entity.HasMany(g => g.UserClaims)
                .WithOne(s => s.User).HasForeignKey(s => s.UserId);

                entity.HasMany(g => g.UserLogins)
                .WithOne(s => s.User).HasForeignKey(s => s.UserId);

                entity.HasMany(g => g.UserRoles)
                .WithOne(s => s.User).HasForeignKey(s => s.UserId);

                entity.HasMany(g => g.UserTokens)
                .WithOne(s => s.User).HasForeignKey(s => s.UserId);

            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable(name: "UserClaim", schema: "security");
                entity.Property(x => x.Id).HasColumnName("UserClaimId");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable(name: "UserLogin", schema: "security");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable(name: "UserToken", schema: "security");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Role", schema: "security");
                entity.Property(x => x.Id).HasColumnName("RoleId");
                entity.Ignore(x => x.Name);
                entity.Ignore(x => x.NormalizedName);
                entity.Ignore(x => x.ConcurrencyStamp);

                entity.HasMany(g => g.RoleClaims)
                .WithOne(s => s.Role).HasForeignKey(s => s.RoleId);

                entity.HasMany(g => g.UserRoles)
               .WithOne(s => s.Role).HasForeignKey(s => s.RoleId);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaim", schema: "security");
                entity.Property(e => e.Id).HasColumnName("RoleClaimId");

            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable(name: "UserRole", schema: "security");
            });
            #endregion

            #region Security View
            modelBuilder.Entity<ServiceAccessView>(entity =>
            {
                entity.HasKey(e => e.ServiceAccessId);
                entity.ToView("ServiceAccessView");
            });

            modelBuilder.Entity<UserRoleView>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);
                entity.ToView("UserRoleView");
            });


            modelBuilder.Entity<RoleServiceAccessView>(entity =>
            {
                entity.HasKey(e => e.RoleServiceAccessId);
                entity.ToView("RoleServiceAccessView");
            });


            modelBuilder.Entity<UserRoleServiceAccessView>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId, e.ServiceId });
                entity.ToView("UserRoleServiceAccessView");
            });

            #endregion
        }


        private void SetViewEntity(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserView>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToView("UserView");
            });

            modelBuilder.Entity<CountryView>(entity =>
            {
                entity.HasKey(e => e.CountryId);
                entity.ToView("CountryView");
            });

            modelBuilder.Entity<StateView>(entity =>
            {
                entity.HasKey(e => e.StateId);
                entity.ToView("StateView");
            });

            modelBuilder.Entity<CityView>(entity =>
            {
                entity.HasKey(e => e.CityId);
                entity.ToView("CityView");
            });

            modelBuilder.Entity<RoutineCategoryView>(entity =>
            {
                entity.HasKey(e => e.RoutineCategoryId);
                entity.ToView("RoutineCategoryView");
            });

            modelBuilder.Entity<RoutineRouteTypeView>(entity =>
            {
                entity.HasKey(e => e.RoutineRouteTypeId);
                entity.ToView("RoutineRouteTypeView");
            });

            modelBuilder.Entity<RoutineView>(entity =>
            {
                entity.HasKey(e => e.RoutineId);
                entity.ToView("RoutineView");
            });

            modelBuilder.Entity<GroupStatusView>(entity =>
            {
                entity.HasKey(e => e.GroupStatusId);
                entity.ToView("GroupStatusView");
            });

            modelBuilder.Entity<GroupView>(entity =>
            {
                entity.HasKey(e => e.GroupId);
                entity.ToView("GroupView");
            });

            modelBuilder.Entity<UserGroupView>(entity =>
            {
                entity.HasKey(e => e.UserGroupId);
                entity.ToView("UserGroupView");
            });

            modelBuilder.Entity<InvitationView>(entity =>
            {
                entity.HasKey(e => e.InvitationId);
                entity.ToView("InvitationView");
            });

        }



        #region Tables
        public virtual DbSet<RoutineCategory> RoutineCategories { get; set; }
        public virtual DbSet<RoutineRouteType> RoutineRouteTypes { get; set; }
        public virtual DbSet<GroupStatus> GroupStatuses { get; set; }
        public virtual DbSet<Routine> Routines { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<RoutineDay> RoutineDays { get; set; }

        #endregion

        #region Security
        public virtual DbSet<AccessType> AccessTypes { get; set; }
        public virtual DbSet<RoleService> RoleServices { get; set; }
        public virtual DbSet<RoleServiceAccess> RoleServiceAccesses { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceAccess> ServiceAccesses { get; set; }
        public virtual DbSet<UserService> UserServices { get; set; }
        public virtual DbSet<UserServiceAccess> UserServiceAccesses { get; set; }

        #endregion

        #region Views
        public virtual DbSet<UserView> UserView { get; set; }
        public virtual DbSet<CountryView> CountryView { get; set; }
        public virtual DbSet<StateView> StateView { get; set; }
        public virtual DbSet<CityView> CityView { get; set; }
        public virtual DbSet<RoutineCategoryView> RoutineCategoryView { get; set; }
        public virtual DbSet<RoutineRouteTypeView> RoutineRouteTypeView { get; set; }
        public virtual DbSet<RoutineView> RoutineView { get; set; }
        public virtual DbSet<GroupStatusView> GroupStatusView { get; set; }
        public virtual DbSet<GroupView> GroupView { get; set; }
        public virtual DbSet<UserGroupView> UserGroupView { get; set; }
        public virtual DbSet<InvitationView> InvitationView { get; set; }
        #endregion

    }
}
