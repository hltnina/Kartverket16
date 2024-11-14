using Microsoft.AspNetCore.Identity;

namespace Nettside.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }  
    }
}
