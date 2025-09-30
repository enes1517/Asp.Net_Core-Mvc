using StoreApp.mail;
using StoreApp.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Repositories.Contracts
{
   public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        IOrderRepository Order { get; }
       
        IEmailSender EmailSender { get; }

        void Save(); 

    }
}
