using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ResetPasswordDto
    {

        public String? UserName { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public String? Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password and ConfirmPassword must be match")]
        public String? ConfirmPassword { get; set; }
    }
}
