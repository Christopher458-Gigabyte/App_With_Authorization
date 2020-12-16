using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App_With_Authorization.Data;
using App_With_Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using App_With_Authorization.Authorization;

namespace App_With_Authorization.Pages.Modules
{
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Module> Module { get; set; }

        public async Task OnGetAsync()
        {
            var contacts = from c in Context.Module
                           select c;

            var isAuthorized = User.IsInRole(Constants.ModuleManagersRole) ||
                               User.IsInRole(Constants.ModuleAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                contacts = contacts.Where(c => c.Status == ModuleStatus.Approved
                                            || c.OwnerID == currentUserId);
            }

            Module = await contacts.ToListAsync();
        }
    }
}
