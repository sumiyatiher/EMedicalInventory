using Microsoft.AspNetCore.Identity;

namespace EMedicalInventory.Models
{
    public class Users : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
