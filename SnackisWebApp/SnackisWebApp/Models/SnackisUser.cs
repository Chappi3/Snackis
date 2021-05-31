using Microsoft.AspNetCore.Identity;

namespace SnackisWebApp.Models
{
    public class SnackisUser : IdentityUser
    {
        public string ProfileImg { get; set; }
    }
}
