using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CategoryGateway _categoryGateway;
        private readonly SubCategoryGateway _subCategoryGateway;

        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public IndexModel(CategoryGateway categoryGateway, SubCategoryGateway subCategoryGateway)
        {
            _categoryGateway = categoryGateway;
            _subCategoryGateway = subCategoryGateway;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _categoryGateway.GetAllCategories();
            SubCategories = await _subCategoryGateway.GetAllSubCategories();

            return Page();
        }
    }
}
