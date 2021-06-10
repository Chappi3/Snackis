using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SnackisWebApp.Pages.Admin.Reports
{
    public class ManageReportModel : PageModel
    {
        private readonly PostGateway _postGateway;
        private readonly ReportGateway _reportGateway;
        private readonly CommentGateway _commentGateway;
        private readonly UserManager<SnackisUser> _userManager;

        [BindProperty]
        public string DeleteReportId { get; set; }

        [BindProperty]
        public string DeletePostId { get; set; }

        [BindProperty]
        public string DeleteCommentId { get; set; }


        public CustomReportModel Report { get; set; }

        public class CustomReportModel
        {
            public string Id { get; set; }
            public SnackisUser ByUser { get; set; }
            public Post Post { get; set; }
            public Comment Comment { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public ManageReportModel(ReportGateway reportGateway, PostGateway postGateway, CommentGateway commentGateway, UserManager<SnackisUser> userManager)
        {
            _userManager = userManager;
            _postGateway = postGateway;
            _reportGateway = reportGateway;
            _commentGateway = commentGateway;
        }

        public async Task<IActionResult> OnGetAsync(string reportId)
        {
            if (reportId == null)
            {
                return NotFound();
            }

            var report = await _reportGateway.GetReportById(reportId);

            if (report.PostId != null)
            {
                Report = new CustomReportModel
                {
                    Id = report.Id,
                    Content = report.Content,
                    CreatedAt = report.CreatedAt,
                    Post = await _postGateway.GetPostById(report.PostId),
                    ByUser = await _userManager.FindByIdAsync(report.ByUser)
                };
            }
            else if (report.CommentId != null)
            {
                Report = new CustomReportModel
                {
                    Id = report.Id,
                    Content = report.Content,
                    CreatedAt = report.CreatedAt,
                    ByUser = await _userManager.FindByIdAsync(report.ByUser),
                    Comment = await _commentGateway.GetCommentById(report.CommentId)
                };
            }
            

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (DeleteReportId != null)
            {
                var result = await _reportGateway.DeleteReport(DeleteReportId);
                if (result)
                {
                    return RedirectToPage("ReportsPage");
                }
            }

            return RedirectToPage(new{reportId = DeleteReportId});
        }

        public async Task<IActionResult> OnPostDeleteComment()
        {
            if (DeleteCommentId != null)
            {
                
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePost()
        {
            if (DeletePostId != null)
            {
                
            }

            return RedirectToPage();
        }
    }
}
