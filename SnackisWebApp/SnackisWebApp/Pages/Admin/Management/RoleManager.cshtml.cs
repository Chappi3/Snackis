using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages.Admin.Management
{
    public class RoleManagerModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<IdentityRole> IdentityRoles { get; set; }

        [BindProperty]
        public Guid RoleId { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }
        }

        public RoleManagerModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IdentityRoles = await _roleManager.Roles.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddRole()
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(Input.Name));
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRole()
        {
            if (RoleId == Guid.Empty) return Page();

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == RoleId.ToString());

            if (role == null) return NotFound();

            await _roleManager.DeleteAsync(role);
            return RedirectToPage();
        }
    }
}
