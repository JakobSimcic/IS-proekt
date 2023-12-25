using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
            public string Email { get; set; }
            public string Password { get; set; }

}