using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;

namespace SnackisWebApp.Pages.User
{
    public class SendMessageModel : PageModel
    {
        private readonly MessageGateway _messageGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public SnackisUser ToUser { get; set; }
        public SnackisUser CurrentUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string ToUserId { get; set; }
            [Required]
            public string FromUser { get; set; }
            [Required]
            public string Content { get; set; }
        }

        public SendMessageModel(MessageGateway messageGateway, UserManager<SnackisUser> userManager)
        {
            _messageGateway = messageGateway;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string toUserId)
        {
            if (toUserId == null) return NotFound();

            ToUser = await _userManager.FindByIdAsync(toUserId);

            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _messageGateway.CreateMessage(Input.FromUser, Input.ToUserId, Input.Content);

                StatusMessage = "Successfully sent message!";
                return RedirectToPage(new {Input.ToUserId});
            }

            StatusMessage = "Failed to send message!";
            return RedirectToPage(new {Input.ToUserId});
        }
    }
}
