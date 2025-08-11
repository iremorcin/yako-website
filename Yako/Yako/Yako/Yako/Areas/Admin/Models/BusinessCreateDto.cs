using System.ComponentModel.DataAnnotations;

namespace Yako.UI.Areas.Admin.Models
{
    public class BusinessCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string MapUrl { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public IFormFile Image { get; set; }
    }
}
