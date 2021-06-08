using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages.Admin.Management
{
    public class SubCategoryManagerModel : PageModel
    {
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly CategoryGateway _categoryGateway;

        public List<SubCategory> SubCategories { get; set; }

        [BindProperty]
        public Category Category { get; set; }

        [BindProperty]
        public string SubCategoryId { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string CategoryId { get; set; }
        }

        public SubCategoryManagerModel(SubCategoryGateway subCategoryGateway, CategoryGateway categoryGateway)
        {
            _subCategoryGateway = subCategoryGateway;
            _categoryGateway = categoryGateway;
        }

        public async Task<IActionResult> OnGetAsync(string categoryId)
        {
            Category = await _categoryGateway.GetCategoryById(categoryId);
            if (Category == null)
            {
                return NotFound();
            }

            SubCategories = await _subCategoryGateway.GetAllSubCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostAddSubCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdSubCategory = await _subCategoryGateway.CreateSubCategory(Input.Name, Input.CategoryId);
            return RedirectToPage(new { categoryId = createdSubCategory.CategoryId });
        }

        public async Task<IActionResult> OnPostDeleteSubCategory()
        {
            // Todo: check for related posts before deleting
            var isSuccess = await _subCategoryGateway.DeleteSubCategory(SubCategoryId);

            if (isSuccess)
            {
                return RedirectToPage(new { categoryId = Category.Id });
            }
            return BadRequest();
        }
    }
}
