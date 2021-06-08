using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages.Admin.Management
{
    public class CategoryManagerModel : PageModel
    {
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;

        public List<Category> Categories { get; set; }

        [BindProperty]
        public string CategoryId { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }
        }

        public CategoryManagerModel(CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway)
        {
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _categoryGateway.GetAllCategories();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAddCategory()
        {
            if (ModelState.IsValid)
            {
                await _categoryGateway.CreateCategory(Input.Name);
                return RedirectToPage();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostDeleteCategory()
        {
            if (CategoryId != null || CategoryId != string.Empty)
            {
                // Todo: create seperate endpoint to get subcategories by categoryId
                var subCategories = await _subCategoryGateway.GetAllSubCategories();
                var relatedSubCategories = subCategories.Where(s => s.CategoryId == CategoryId).ToList();

                if (relatedSubCategories.Count == 0)
                {
                    var isSuccess = await _categoryGateway.DeleteCategory(CategoryId);

                    if (isSuccess)
                    {
                        return RedirectToPage();
                    }
                }
            }
            return BadRequest();
        }
    }
}
