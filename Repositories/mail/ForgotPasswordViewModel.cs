using System.ComponentModel.DataAnnotations;

namespace StoreApp.mail
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool SendResetEmail { get; set; } // Mail gönderilsin mi?
    }
}
