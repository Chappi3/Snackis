using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages.User
{
    public class MyMessagesModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly MessageGateway _messageGateway;

        public string UserId { get; set; }

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

        public MyMessagesModel(UserManager<SnackisUser> userManager, MessageGateway messageGateway)
        {
            _userManager = userManager;
            _messageGateway = messageGateway;
            Messages = new List<CustomMessageModel>();
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null) return NotFound();
            UserId = userId;

            var messages = await _messageGateway.GetAllMessagesByUserId(UserId);
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
            if (ModelState.IsValid)
            {
                await _messageGateway.EditMessage(Message.Id, Message);
                return RedirectToPage(new {userId = Message.ToUser});
            }

            return BadRequest();
        }
    }
}
