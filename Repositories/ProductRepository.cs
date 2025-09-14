using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using StoreApp.Infrastructe.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneProduct(Product product)=>Create(product);

        public void DeleteOneProduct(Product product) => Remove(product);
        

        public IQueryable<Product> GetAllProducts(bool trackChanges)=>FindAll(trackChanges);

        public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
        {
           return _context
                .Products
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchTerm(p.SearchTerm)
                .FilteredByPrice(p.MinPrice,p.MaxPrice,p.IsValidPrice)
                .ToPaginate(p.PageNumber,p.PageSize);
        }

        public IQueryable<Product> GetLastestProducts(int n,bool trackChanges)
        {
            return FindAll(trackChanges)
                   .OrderByDescending(prd=>prd.ProductId)
                   .Take(n);

        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return FindByCondation(p => p.ProductId.Equals(id),trackChanges);

        }

        public IQueryable<Product> GetShowCaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges)
                .Where(s=>s.ShowCase.Equals(true));
        }

        public void UpdateOneProduct(Product entity) => Update(entity);

       
    }
}
