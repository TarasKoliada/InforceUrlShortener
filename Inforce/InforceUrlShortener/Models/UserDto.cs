using InforceUrlShortener.Core.Services;
using InforceUrlShortener.Services;
using System.Collections.Generic;

namespace InforceUrlShortener.Models
{
    public class UserDto
    {
        public UserDto()
        {
            Id = IdentifierProvider.GetGuid();
        }
        private string _passwodHash;
        public string Id { get; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string PasswordHash
        {
            get { return _passwodHash; }
            set { _passwodHash = Password.HashPassword(value); }
        }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public ICollection<ShortUrlDto> ShortUrls { get; set; }
    }
}
