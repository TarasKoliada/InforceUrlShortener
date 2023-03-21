using System;

namespace InforceUrlShortener.Models
{
    public class ShortUrlDto
    {
        public ShortUrlDto()
        {
            GuidId = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string GuidId { get;}
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }

        public string CreatedByUserId { get; set; }
        public DateTime CreationDate { get;}
        public UserDto User { get; set; }
    }
}
