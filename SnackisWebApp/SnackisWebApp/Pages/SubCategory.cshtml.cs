using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages
{
    public class SubCategoryModel : PageModel
    {
        public UserManager<SnackisUser> UserManager { get; }
        private readonly PostGateway _postGateway;
        private readonly SubCategoryGateway _subCategoryGateway;

        public string PostId { get; set; }
        public string UserId { get; set; }
        public List<Post> Posts { get; set; }
        public SubCategory SubCategory { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string Content { get; set; }
            [Required]
            public string SubCategoryId { get; set; }
            [Required]
            public string UserId { get; set; }
        }

        public SubCategoryModel(PostGateway postGateway, SubCategoryGateway subCategoryGateway, UserManager<SnackisUser> userManager)
        {
            UserManager = userManager;
            _postGateway = postGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task<IActionResult> OnGetAsync(string subcategoryId)
        {
            Posts = await _postGateway.GetAllPostsBySubcategoryId(subcategoryId);
            SubCategory = await _subCategoryGateway.GetSubcategoryById(subcategoryId);
            UserId = UserManager.GetUserId(User);

            if (SubCategory == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _postGateway.CreatePost(Input.Title, Input.Content, Input.UserId, Input.SubCategoryId);

                if (result != null)
                {
                    return RedirectToPage(new { subcategoryId = result.SubCategoryId });
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> OnPostDeletePost()
        {
            throw new NotImplementedException();
        }
    }
}
