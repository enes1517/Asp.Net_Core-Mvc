using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class LoginModel
    {
        private string _returnurl;
        [Required(ErrorMessage ="Name is Required")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
        
        public string ReturnUrl
        {
            get
            {
                if (_returnurl is null)
                    return  "/";
                else
                    return _returnurl;
            }
            set
            {
               _returnurl = value;
            }
        }
    }
}
