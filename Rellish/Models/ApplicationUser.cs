using Microsoft.AspNetCore.Identity;

namespace Rellish.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
