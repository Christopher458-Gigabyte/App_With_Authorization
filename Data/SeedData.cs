using App_With_Authorization.Models;
using App_With_Authorization.Authorization;
using App_With_Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App_With_Authorization.Data
{
    public static class SeedData
    {
        #region snippet_Initialize
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.ModuleAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager1234@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ModuleManagersRole);

                SeedDB(context, adminID);
                SeedDB(context, managerID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        #endregion
       
        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Module.Any())
            {
                return;   // DB has been seeded
            }

            context.Module.AddRange(
          
         /*       new Module
                {
                    Modulnummer= 451812,
                    Modulname = "Grundlagen der Wirtschaftsinformatik",
                    Semester = 1, 
                    Workload = 2 , 
                    Semesterwochenstunden = 1, 
                    Leistungspunkte = 2, 
                    Turnus = "BLA BLA", 
                    Curriculare_Zuordnung = "BLA BLA",
                    Lernziele = "BLA BLA", 
                    Schlüsselqualifikationen = "BLA BLA",
                    Inhaltliche_Beschreibung = "BLA BLA", 
                    Unterrichtsform = "BLA BLA", 
                    Prüfungsart = "BLA BLA", 
                    Prüfungsform = "BLA BLA", 
                    Verbindlichkeit = "BLA BLA", 
                    Literaturangaben = "BLA BLA", 
                    ReleaseDate = new DateTime(2016, 5, 2),
                    Status = ModuleStatus.Approved,
                    OwnerID = adminID



                },
            #endregion
            #endregion
             new Module
             {
                 Modulnummer = 451812,
                 Modulname = "Grundlagen der Wirtschaftsinformatik",
                 Semester = 1,
                 Workload = 2,
                 Semesterwochenstunden = 1,
                 Leistungspunkte = 2,
                 Turnus = "BLA BLA",
                 Curriculare_Zuordnung = "BLA BLA",
                 Lernziele = "BLA BLA",
                 Schlüsselqualifikationen = "BLA BLA",
                 Inhaltliche_Beschreibung = "BLA BLA",
                 Unterrichtsform = "BLA BLA",
                 Prüfungsart = "BLA BLA",
                 Prüfungsform = "BLA BLA",
                 Verbindlichkeit = "BLA BLA",
                 Literaturangaben = "BLA BLA",
                 ReleaseDate = new DateTime(2016, 5, 2),
                 Status = ModuleStatus.Approved,
                 OwnerID = adminID
             },
              new Module
              {
                  Modulnummer = 451812,
                  Modulname = "Grundlagen der Wirtschaftsinformatik",
                  Semester = 1,
                  Workload = 2,
                  Semesterwochenstunden = 1,
                  Leistungspunkte = 2,
                  Turnus = "BLA BLA",
                  Curriculare_Zuordnung = "BLA BLA",
                  Lernziele = "BLA BLA",
                  Schlüsselqualifikationen = "BLA BLA",
                  Inhaltliche_Beschreibung = "BLA BLA",
                  Unterrichtsform = "BLA BLA",
                  Prüfungsart = "BLA BLA",
                  Prüfungsform = "BLA BLA",
                  Verbindlichkeit = "BLA BLA",
                  Literaturangaben = "BLA BLA",
                  ReleaseDate = new DateTime(2016, 5, 2),
                  Status = ModuleStatus.Approved,
                  OwnerID = adminID
              },
              new Module
              {
                  Modulnummer = 451812,
                  Modulname = "Grundlagen der Wirtschaftsinformatik",
                  Semester = 1,
                  Workload = 2,
                  Semesterwochenstunden = 1,
                  Leistungspunkte = 2,
                  Turnus = "BLA BLA",
                  Curriculare_Zuordnung = "BLA BLA",
                  Lernziele = "BLA BLA",
                  Schlüsselqualifikationen = "BLA BLA",
                  Inhaltliche_Beschreibung = "BLA BLA",
                  Unterrichtsform = "BLA BLA",
                  Prüfungsart = "BLA BLA",
                  Prüfungsform = "BLA BLA",
                  Verbindlichkeit = "BLA BLA",
                  Literaturangaben = "BLA BLA",
                  ReleaseDate = new DateTime(2016, 5, 2),
                  Status = ModuleStatus.Rejected,
                  OwnerID = adminID
              },
              new Module
              {
                  Modulnummer = 451812,
                  Modulname = "Grundlagen der Wirtschaftsinformatik",
                  Semester = 1,
                  Workload = 2,
                  Semesterwochenstunden = 1,
                  Leistungspunkte = 2,
                  Turnus = "BLA BLA",
                  Curriculare_Zuordnung = "BLA BLA",
                  Lernziele = "BLA BLA",
                  Schlüsselqualifikationen = "BLA BLA",
                  Inhaltliche_Beschreibung = "BLA BLA",
                  Unterrichtsform = "BLA BLA",
                  Prüfungsart = "BLA BLA",
                  Prüfungsform = "BLA BLA",
                  Verbindlichkeit = "BLA BLA",
                  Literaturangaben = "BLA BLA",
                  ReleaseDate = new DateTime(2016, 5, 2),
                  Status = ModuleStatus.Submitted,
                  OwnerID = adminID
              }*/
             );
            context.SaveChanges();
        }
    }
}
