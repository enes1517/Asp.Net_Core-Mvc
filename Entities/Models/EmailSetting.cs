using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class EmailSetting
    {
        public int Id { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; } // Güvenlik için şifrelenmiş saklanmalı
        public bool EnableSsl { get; set; }
    }
}
