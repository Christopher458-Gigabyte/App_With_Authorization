using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App_With_Authorization.Data;
using App_With_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using App_With_Authorization.Authorization;

namespace App_With_Authorization.Pages.Modules
{
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Module Module { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Module = await Context.Module.FirstOrDefaultAsync(m => m.ID == id);

            if (Module == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.ModuleManagersRole) ||
                               User.IsInRole(Constants.ModuleAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Module.OwnerID
                && Module.Status != ModuleStatus.Approved)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, ModuleStatus status)
        {
            var contact = await Context.Module.FirstOrDefaultAsync(
                                                      m => m.ID == id);

            if (contact == null)
            {
                return NotFound();
            }

            var contactOperation = (status == ModuleStatus.Approved)
                                                       ? ModuleOperations.Approve
                                                       : ModuleOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, contact,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            contact.Status = status;
            Context.Module.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
