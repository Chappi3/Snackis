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
        private readonly PostGateway _postGateway;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public string UserId { get; set; }
        public SubCategory SubCategory { get; set; }

        [BindProperty]
        public string PostId { get; set; }
        [BindProperty]
        public string SubcategoryId { get; set; }

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

        public List<CustomPostModel> Posts { get; set; }

        public class CustomPostModel
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public DateTime CreatedAt { get; set; }
            public SnackisUser CreatedByUser { get; set; }
        }

        public SubCategoryModel(PostGateway postGateway, SubCategoryGateway subCategoryGateway, UserManager<SnackisUser> userManager)
        {
            _postGateway = postGateway;
            _subCategoryGateway = subCategoryGateway;
            _userManager = userManager;
            Posts = new List<CustomPostModel>();
        }

        public async Task<IActionResult> OnGetAsync(string subcategoryId)
        {
            var posts = await _postGateway.GetAllPostsBySubcategoryId(subcategoryId);
            SubCategory = await _subCategoryGateway.GetSubcategoryById(subcategoryId);
            UserId = _userManager.GetUserId(User);

            if (SubCategory == null)
            {
                return NotFound();
            }

            foreach (var post in posts)
            {
                var customPostModel = new CustomPostModel
                {
                    Id = post.Id,
                    CreatedByUser = await _userManager.FindByIdAsync(post.UserId),
                    Title = post.Title,
                    CreatedAt = post.CreatedAt
                };
                Posts.Add(customPostModel);
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
            var result = await _postGateway.DeletePost(PostId);

            if (result)
            {
                return RedirectToPage(new { subcategoryId = SubcategoryId });
            }

            return BadRequest();
        }
    }
}
