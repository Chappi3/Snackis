using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages
{
    public class SubCategoryModel : PageModel
    {
        public List<Post> Posts { get; set; }

        public string PostId { get; set; }

        public SubCategory SubCategory { get; set; }

        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Name { get; set; }
            public string SubCategoryId { get; set; }
        }

        public void OnGet()
        {
        }
    }
}
