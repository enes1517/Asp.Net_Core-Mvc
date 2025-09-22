using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record class UserDto
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="UserName is required")]
        public String? UserName { get; init; }

        [DataType(DataType.PhoneNumber)]

        [Required(ErrorMessage = "PhoneNumber is required")]
        public String PhoneNumber { get; init; }

        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "Email is required")]
        public String? Email { get; init; }
        public HashSet<String> Roles { get; set; } = new HashSet<String>();
    }
}
