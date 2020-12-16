using App_With_Authorization.Authorization;
using App_With_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using App_With_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using App_With_Authorization.Authorization;
using App_With_Authorization.Data;
using App_With_Authorization.Models;





namespace App_With_Authorization.Pages.Modules
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
          ApplicationDbContext context,
          IAuthorizationService authorizationService,
          UserManager<IdentityUser> userManager)
          : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
           /* Module = new Module
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
                ReleaseDate = new DateTime(2016, 5, 2)
            };*/
            return Page();



        }

        [BindProperty]
        public Module Module { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Module.Modulname = UserManager.GetUserId(User);

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Module,
                                                        ModuleOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Module.Add(Module);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
