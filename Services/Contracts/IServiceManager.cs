using StoreApp.mail;
using StoreApp.Mail;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService {get;}
        ICategoryService CategoryService {get;}
        IOrderService OrderService {get;}
        IAuthService AuthService {get;}
        IEmailSender EmailSender {get;}
    }
}