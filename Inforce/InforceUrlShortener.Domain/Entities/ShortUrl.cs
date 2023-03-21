using System;
using System.ComponentModel.DataAnnotations;

namespace InforceUrlShortener.Domain.Entities
{
    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }
        public string GuidId { get; set; }
        [Required]
        [MaxLength]
        public string OriginalUrl { get; set; }
        [Required]
        [MaxLength]
        public string ShortenedUrl { get; set; }
        [Required]
        public string CreatedByUserId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public virtual User User { get; set; }
    }
}
