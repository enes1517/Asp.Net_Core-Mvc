using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record class UserDtoForCreation:UserDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public String? Password { get; set; }
    
    }
}
