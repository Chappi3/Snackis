using System;
using System.Collections.Generic;
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
        private readonly UserManager<SnackisUser> _userManager;
        private readonly CommentGateway _commentGateway;

        public Post Post { get; set; }
        public SnackisUser CreatedByUser { get; set; }

        public List<CustomCommentModel> Comments { get; set; }

        public class CustomCommentModel
        {
            public string Id { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public SnackisUser CreatedByUser { get; set; }
        }

        public PostPageModel(PostGateway postGateway, UserManager<SnackisUser> userManager, CommentGateway commentGateway)
        {
            _postGateway = postGateway;
            _userManager = userManager;
            _commentGateway = commentGateway;
        }

        public async Task<IActionResult> OnGetAsync(string postId)
        {
            Post = await _postGateway.GetPostById(postId);
            if (Post == null)
            {
                return NotFound();
            }

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
    }
}
