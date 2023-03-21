using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;

namespace InforceUrlShortener.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DefaultValue("User")]
        public string Role { get; set; }
        public virtual ICollection<ShortUrl> ShortUrls { get; set; }

    }
}
