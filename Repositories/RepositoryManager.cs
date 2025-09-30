using Repositories.Contracts;
using StoreApp.mail;
using StoreApp.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly RepositoryContext _context;
        private readonly IEmailSender _mail;


        public RepositoryManager(IProductRepository productRepository, RepositoryContext repositoryContext, ICategoryRepository categoryRepository, IOrderRepository orderRepository, IEmailSender mail)
        {
            _productRepository = productRepository;
            _context = repositoryContext;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _mail = mail;
        }

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public IOrderRepository Order => _orderRepository;

        public IEmailSender EmailSender => _mail;

        public void Save() => _context.SaveChanges();

    }
}
