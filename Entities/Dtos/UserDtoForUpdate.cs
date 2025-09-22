using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record class UserDtoForUpdate:UserDto
    {
        public HashSet<String> UserRoles { get; set; } = new();

    }
}
