using Microsoft.AspNetCore.Identity;

namespace SnackisWebApp.Models
{
    public class SnackisUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfileImg { get; set; }
    }
}
