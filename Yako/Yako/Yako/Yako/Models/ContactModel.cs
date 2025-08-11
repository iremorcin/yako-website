using System.ComponentModel.DataAnnotations;

namespace Yako.UI.Models
{
    public class ContactModel
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mesaj kısmı boş bırakılamaz.")]
        public string Message { get; set; }
    }
}
