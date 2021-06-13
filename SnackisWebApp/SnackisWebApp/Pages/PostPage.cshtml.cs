using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages
{
    public class PostPageModel : PageModel
    {
        private readonly PostGateway _postGateway;
        private readonly CommentGateway _commentGateway;
        private readonly ReportGateway _reportGateway;
        private readonly MessageGateway _messageGateway;
        private readonly SubCategoryGateway _subCategoryGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public Post Post { get; set; }
        public string UserId { get; set; }
        public SubCategory SubCategory { get; set; }
        public SnackisUser CreatedByUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public List<CustomCommentModel> Comments { get; set; }

        public class CustomCommentModel
        {
            public string Id { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public SnackisUser CreatedByUser { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Content { get; set; }
            [Required]
            public string PostId { get; set; }
            [Required]
            public string UserId { get; set; }
        }

        [BindProperty]
        public ReportInputModel ReportInput { get; set; }

        public class ReportInputModel
        {
            [Required]
            public string ByUser { get; set; }
            [Required]
            public string Content { get; set; }
            [Required]
            public string ReturnPostId { get; set; }

            public string PostId { get; set; }
            public string CommentId { get; set; }
        }

        [BindProperty] 
        public MessageInputModel MessageInput { get; set; }

        public class MessageInputModel
        {
            [Required]
            public string ToUserId { get; set; }
            [Required]
            public string FromUserId { get; set; }
            [Required]
            public string Content { get; set; }
            [Required]
            public string ReturnPostId { get; set; }
        }

        public PostPageModel(PostGateway postGateway, UserManager<SnackisUser> userManager, CommentGateway commentGateway, ReportGateway reportGateway, MessageGateway messageGateway, SubCategoryGateway subCategoryGateway)
        {
            _postGateway = postGateway;
            _userManager = userManager;
            _commentGateway = commentGateway;
            _reportGateway = reportGateway;
            _messageGateway = messageGateway;
            _subCategoryGateway = subCategoryGateway;
            Comments = new List<CustomCommentModel>();
        }

        public async Task<IActionResult> OnGetAsync(string postId)
        {
            Post = await _postGateway.GetPostById(postId);
            if (Post == null)
            {
                return NotFound();
            }

            SubCategory = await _subCategoryGateway.GetSubcategoryById(Post.SubCategoryId);
            UserId = _userManager.GetUserId(User);
            CreatedByUser = await _userManager.FindByIdAsync(Post.UserId);
            var comments = await _commentGateway.GetCommentByPostId(postId);

            foreach (var comment in comments)
            {
                var customComment = new CustomCommentModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    CreatedByUser = await _userManager.FindByIdAsync(comment.UserId)
                };
                Comments.Add(customComment);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddComment()
        {
            if (Input.UserId != null && Input.PostId != null && (Input.Content != null || Input.Content != string.Empty))
            {
                var result = await _commentGateway.CreateComment(Input.Content, Input.PostId, Input.UserId);

                if (result)
                {
                    StatusMessage = "Successfully created comment!";
                    return RedirectToPage(new { postId = Input.PostId });
                }
            }

            StatusMessage = "Failed to create comment!";
            return RedirectToPage(new { postId = Input.PostId });
        }

        public async Task<IActionResult> OnPostCreateReport()
        {
            if (ReportInput.ByUser != null && ReportInput.Content != null && ReportInput.ReturnPostId != null && (ReportInput.PostId != null || ReportInput.CommentId != null))
            {
                var result = await _reportGateway.CreateReport(ReportInput.Content, ReportInput.ByUser, ReportInput.PostId, ReportInput.CommentId);

                if (result)
                {
                    StatusMessage = "Successfully sent report!";
                    return RedirectToPage(new {postId = ReportInput.ReturnPostId });
                }
            }

            StatusMessage = "Failed to send report!";
            return RedirectToPage(new { postId = ReportInput.ReturnPostId });
        }

        public async Task<IActionResult> OnPostSendMessage()
        {
            if (MessageInput.FromUserId != null && MessageInput.ToUserId != null && MessageInput.ReturnPostId != null && (MessageInput.Content != null || MessageInput.Content != string.Empty))
            {
                var result = await _messageGateway.CreateMessage(MessageInput.FromUserId, MessageInput.ToUserId, MessageInput.Content);

                if (result)
                {
                    StatusMessage = "Successfully sent message!";
                    return RedirectToPage(new {postId = MessageInput.ReturnPostId});
                }
            }

            StatusMessage = "Failed to send message!";
            return RedirectToPage(new { postId = MessageInput.ReturnPostId });
        }
    }
}
