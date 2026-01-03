using Microsoft.AspNetCore.Identity;

namespace shared.Models
{
    public class User : IdentityUser
    {
        public string NomeCompleto { get; set; }

        public string NIF { get; set; }

        public bool IsCurador { get; set; }
    }
}
