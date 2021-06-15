using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SnackisWebApp.Pages.Admin.Reports
{
    public class ReportsPageModel : PageModel
    {
        private readonly PostGateway _postGateway;
        private readonly ReportGateway _reportGateway;
        private readonly CommentGateway _commentGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public List<CustomReportModel> Reports { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string ReportId { get; set; }

        public class CustomReportModel
        {
            public string Id { get; set; }
            public Post Post { get; set; }
            public string Content { get; set; }
            public Comment Comment { get; set; }
            public DateTime CreatedAt { get; set; }
            public SnackisUser ByUser { get; set; }
        }

        public ReportsPageModel(ReportGateway reportGateway, UserManager<SnackisUser> userManager, PostGateway postGateway, CommentGateway commentGateway)
        {
            _userManager = userManager;
            _postGateway = postGateway;
            _reportGateway = reportGateway;
            _commentGateway = commentGateway;
            Reports = new List<CustomReportModel>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var reports = await _reportGateway.GetAllReports();

            foreach (var report in reports)
            {
                if (report.PostId != null || report.CommentId != null)
                {
                    var post = await _postGateway.GetPostById(report.PostId);
                    var comment = await _commentGateway.GetCommentById(report.CommentId);

                    var customReport = new CustomReportModel
                    {
                        Id = report.Id,
                        Content = report.Content,
                        CreatedAt = report.CreatedAt,
                        ByUser = await _userManager.FindByIdAsync(report.ByUser)
                    };

                    if (post != null)
                    {
                        customReport.Post = post;
                        Reports.Add(customReport);
                    }
                    else if (comment != null)
                    {
                        customReport.Comment = comment;
                        Reports.Add(customReport);
                    }
                    else
                    {
                        await _reportGateway.DeleteReport(report.Id);
                        break;
                    }
                }
                else
                {
                    await _reportGateway.DeleteReport(report.Id);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteReport()
        {
            if (ReportId != null)
            {
                var result = await _reportGateway.DeleteReport(ReportId);
                if (result)
                {
                    StatusMessage = $"Successfully deleted report! ({ReportId})";
                    return RedirectToPage();
                }
            }

            StatusMessage = $"Error Failed to delete report! ({ReportId})";
            return RedirectToPage();
        }
    }
}
