using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages
{
    public class CreateReportModel : PageModel
    {
        private readonly PostGateway _postGateway;
        private readonly ReportGateway _reportGateway;
        private readonly CommentGateway _commentGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public SnackisUser CurrentUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PostId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CommentId { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        
        public class InputModel
        {
            [Required]
            public string ByUser { get; set; }
            [Required]
            public string Content { get; set; }

            public string PostId { get; set; }
            public string CommentId { get; set; }
        }

        public CreateReportModel(ReportGateway reportGateway, PostGateway postGateway, CommentGateway commentGateway, UserManager<SnackisUser> userManager)
        {
            _reportGateway = reportGateway;
            _postGateway = postGateway;
            _commentGateway = commentGateway;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (PostId == null && CommentId == null) return NotFound();

            if (PostId != null)
            {
                Post = await _postGateway.GetPostById(PostId);
            }

            if (CommentId != null)
            {
                Comment = await _commentGateway.GetCommentById(CommentId);
            }

            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && (Input.PostId != null || Input.CommentId != null))
            {
                var result = await _reportGateway.CreateReport(Input.Content, Input.ByUser, Input.PostId, Input.CommentId);
                if (result)
                {
                    return RedirectToPage("Index");
                }
            }

            StatusMessage = "Error Failed to create report!";
            return RedirectToPage(new {Input.PostId, Input.CommentId});
        }
    }
}
