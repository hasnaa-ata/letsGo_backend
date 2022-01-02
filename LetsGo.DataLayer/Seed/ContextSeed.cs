using DataLayer.Security.IdentityStore;
using DataLayer.Security.TableEntity;
using LetsGo.DataLayer;
using LetsGo.DataLayer.TableEntity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Seed
{
    public static class ContextSeed
    {
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager)
        {
            LetsGoDBContext context = new LetsGoDBContext();

            //Seed Default User
            var defaultUser = new User
            {
                Id = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                UserName = "simsim_admin@simsim.com",
                Email = "simsim_admin@simsim.com",
                UserFullName = "Ahmed Shams",
                UserAltFullName = "احمد شمس",
                JobTitle = "CEO",
                WorkPlace = "SIMSIM it",
                EmailConfirmed = true,
                PhoneNumber = "+201066171376",
                CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                CreateDate = DateTime.Now
            };

            if (!userManager.Users.Any(u => u.Id == defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Sh@ms2018");
                }
            }

            if (!context.RoutineCategories.Any())
            {

                List<RoutineCategory> category_lst = new List<RoutineCategory>()
                {
                    new RoutineCategory()
                    {
                        RoutineCategoryId = new Guid("BB171A9B-0C0A-4F7C-B9B1-A65ED4E057F2"),
                        RoutineCategoryName = "Sports",
                        RoutineCategoryAltName = "رياضه",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    },
                    new RoutineCategory()
                    {
                        RoutineCategoryId = new Guid("591D7227-2ABE-45E5-81BF-B11845501F85"),
                        RoutineCategoryName = "Eating",
                        RoutineCategoryAltName = "الاكل",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    },
                    new RoutineCategory()
                    {
                        RoutineCategoryId = new Guid("8A6BD080-1EA8-41C2-924C-00F4828B7E4C"),
                        RoutineCategoryName = "Work",
                        RoutineCategoryAltName = "العمل",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    },
                    new RoutineCategory()
                    {
                        RoutineCategoryId = new Guid("9D4694D2-CFF6-472E-BCEF-DFD90E8B6D65"),
                        RoutineCategoryName = "Picnic",
                        RoutineCategoryAltName = "نزهه",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    }

                };
                context.RoutineCategories.AddRange(category_lst);
                context.SaveChanges();
            }

            if (!context.GroupStatuses.Any())
            {
                List<GroupStatus> groupStatus_lst = new List<GroupStatus>()
                {
                    new GroupStatus()
                    {
                        GroupStatusId = new Guid("B947E28E-F580-43B0-A197-5AFF63D3944E"),
                        GroupStatusName = "Private",
                        GroupStatusAltName = "خاص",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    },
                    new GroupStatus()
                    {
                        GroupStatusId = new Guid("C240C632-5E62-4D0C-9F4D-763673831ED6"),
                        GroupStatusName = "Public",
                        GroupStatusAltName = "عام",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    }

                };
                context.GroupStatuses.AddRange(groupStatus_lst);
                context.SaveChanges();
            }

            if (!context.RoutineRouteTypes.Any())
            {
                List<RoutineRouteType> routineRouteTypes_lst = new List<RoutineRouteType>()
                {
                    new RoutineRouteType()
                    {
                        RoutineRouteTypeId = new Guid("A8C38364-0F78-4014-A1E1-F4F5E4050D75"),
                        RoutineRouteTypeName = "One Side",
                        RoutineRouteTypeAltName = "جهه واحده",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    },
                    new RoutineRouteType()
                    {
                        RoutineRouteTypeId = new Guid("9DBA3795-320F-49EB-A1CB-EF1AE330CBE5"),
                        RoutineRouteTypeName = "Two Sides",
                        RoutineRouteTypeAltName = "جهتان",
                        CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                        CreateDate = DateTime.Now
                    }

                };
                context.RoutineRouteTypes.AddRange(routineRouteTypes_lst);
                context.SaveChanges();
            }

            //if (!context.Routines.Any())
            //{
            //    List<Routine> routines_lst = new List<Routine>()
            //    {
            //        new Routine()
            //        {
            //            RoutineId = new Guid("9793821E-F574-452A-BEC6-D675A7998304"),
            //            RoutineRouteTypeId = new Guid("A8C38364-0F78-4014-A1E1-F4F5E4050D75"),
            //            CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
            //            CreateDate = DateTime.Now
            //        },
            //        new Routine()
            //        {
            //            RoutineId = new Guid("9793821E-F574-452A-BEC6-D675A7998304"),
            //            RoutineName = "Walking",
            //            RoutineAltName = "المشى",
            //            RoutineCategoryId = new Guid("BB171A9B-0C0A-4F7C-B9B1-A65ED4E057F2"),
            //            RoutineRouteTypeId = new Guid("9DBA3795-320F-49EB-A1CB-EF1AE330CBE5"),
            //            CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
            //            CreateDate = DateTime.Now
            //        }

            //    };
            //    context.Routines.AddRange(routines_lst);
            //    context.SaveChanges();
            //}

            RoleStore roleStore = new RoleStore(context);

            if (!roleStore.Roles.Any())
            {
                List<Role> role_lst = new List<Role>()
            {
                new Role()
                {
                    Id = new Guid("8A222BBC-3FF3-4408-B21F-65E6F463C16B"),
                    //Name =  "Super Administrator",
                    RoleName = "Super Administrator",
                    RoleAltName = "مدير",
                    CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                    CreateDate = DateTime.Now
                },
                new Role()
                {
                    Id = new Guid("8DC868CD-73C8-4CC3-A8C7-13AE70899E58"),
                    //Name =  "Group Admin",
                    RoleName = "Group Admin",
                    RoleAltName = "مدير المجموعة",
                    CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                    CreateDate = DateTime.Now
                },
                new Role()
                {
                    Id = new Guid("7010CAA3-9DF0-4A16-B63A-B98BDCD1BDFE"),
                    //Name =  "User",
                    RoleName = "User",
                    RoleAltName = "مستخدم",
                    CreateUserId = new Guid("AC567C30-9D03-4C76-8A1D-F482A5534B25"),
                    CreateDate = DateTime.Now
                },
            };

                foreach (Role role in role_lst)
                {
                    await roleStore.CreateAsync(role);
                }

                //context.Roles.AddRange(role_lst);
                //context.SaveChanges();
            }

            //userManager.AddToRoleAsync(defaultUser, "Super Administrator").Wait();

        }
    }
}
