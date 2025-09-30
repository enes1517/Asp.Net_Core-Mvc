using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Person
    {
        
            public int Id { get; set; }
            public string Email { get; set; }
            public string ResetToken { get; set; }
            public DateTime? ResetTokenExpiration { get; set; }
       
    }
}
