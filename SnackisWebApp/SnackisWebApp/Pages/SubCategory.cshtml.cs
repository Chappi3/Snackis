using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages
{
    public class SubCategoryModel : PageModel
    {
        private readonly PostGateway _postGateway;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public string PostId { get; set; }
        public InputModel Input { get; set; }
        public List<Post> Posts { get; set; }
        public SubCategory SubCategory { get; set; }

        public class InputModel
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string SubCategoryId { get; set; }
        }

        public SubCategoryModel(PostGateway postGateway, SubCategoryGateway subCategoryGateway, UserManager<SnackisUser> userManager)
        {
            _postGateway = postGateway;
            _subCategoryGateway = subCategoryGateway;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string subcategoryId)
        {
            Posts = await _postGateway.GetAllPostsBySubcategoryId(subcategoryId);
            SubCategory = await _subCategoryGateway.GetSubcategoryById(subcategoryId);

            if (SubCategory == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddPost()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostDeletePost()
        {
            throw new NotImplementedException();
        }
    }
}
