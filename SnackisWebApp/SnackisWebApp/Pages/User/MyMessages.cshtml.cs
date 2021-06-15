using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages.User
{
    public class MyMessagesModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly MessageGateway _messageGateway;
        private readonly SignInManager<SnackisUser> _signInManager;

        [BindProperty]
        public Message Message { get; set; }

        public List<CustomMessageModel> Messages { get; set; }

        public class CustomMessageModel
        {
            public string Id { get; set; }
            public SnackisUser FromUser { get; set; }
            public SnackisUser CurrentUser { get; set; }
            public string Content { get; set; }
            public DateTime SentAt { get; set; }
            public bool IsRead { get; set; }
        }

        [BindProperty]
        public MessageInputModel MessageInput { get; set; }

        public class MessageInputModel
        {
            [Required] public string ToUserId { get; set; }
            [Required] public string FromUserId { get; set; }
            [Required] public string Content { get; set; }
        }

        public MyMessagesModel(UserManager<SnackisUser> userManager, MessageGateway messageGateway, SignInManager<SnackisUser> signInManager)
        {
            _userManager = userManager;
            _messageGateway = messageGateway;
            _signInManager = signInManager;
            Messages = new List<CustomMessageModel>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToPage("Login");
            var userId = _userManager.GetUserId(User);
            if (userId == null) return NotFound();

            var messages = await _messageGateway.GetAllMessagesByUserId(userId);
            foreach (var message in messages)
            {
                var customMessage = new CustomMessageModel
                {
                    Id = message.Id,
                    Content = message.Content,
                    SentAt = message.SentAt,
                    IsRead = message.IsRead,
                    FromUser = await _userManager.FindByIdAsync(message.FromUser),
                    CurrentUser = await _userManager.FindByIdAsync(message.ToUser)
                };
                Messages.Add(customMessage);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Message.Id != null && Message.FromUser != null && Message.ToUser != null && Message.Content != null && Message.IsRead)
            {
                await _messageGateway.EditMessage(Message.Id, Message);
                return RedirectToPage(new {userId = Message.ToUser});
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostReplyMessage()
        {
            if (MessageInput.Content != null && MessageInput.FromUserId != null && MessageInput.ToUserId != null)
            {
                var result = await _messageGateway.CreateMessage(MessageInput.FromUserId, MessageInput.ToUserId, MessageInput.Content);
                if (result)
                {
                    return RedirectToPage(new { userId = MessageInput.FromUserId });
                }
            }

            return BadRequest();
        }
    }
}
