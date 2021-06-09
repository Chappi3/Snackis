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
        private readonly UserManager<SnackisUser> _userManager;

        public Post Post { get; set; }
        public string UserId { get; set; }
        public SnackisUser CreatedByUser { get; set; }

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

        public PostPageModel(PostGateway postGateway, UserManager<SnackisUser> userManager, CommentGateway commentGateway)
        {
            _postGateway = postGateway;
            _userManager = userManager;
            _commentGateway = commentGateway;
            Comments = new List<CustomCommentModel>();
        }

        public async Task<IActionResult> OnGetAsync(string postId)
        {
            Post = await _postGateway.GetPostById(postId);
            if (Post == null)
            {
                return NotFound();
            }

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
            if (ModelState.IsValid)
            {
                var status = await _commentGateway.CreateComment(Input.Content, Input.PostId, Input.UserId);

                if (status)
                {
                    return RedirectToPage(new{ postId = Input.PostId });
                }
            }

            return BadRequest();
        }
    }
}
