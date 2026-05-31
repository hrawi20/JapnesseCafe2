using System;
using System.ComponentModel.DataAnnotations;

namespace JapnesseCafe.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }

        [Required, StringLength(600)]
        public string Description { get; set; }

        [Range(0.01, 9999)]
        public decimal Price { get; set; }

        [Required, StringLength(60)]
        public string Category { get; set; }

        public byte[] ImageData { get; set; }

        [StringLength(80)]
        public string ImageMimeType { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
