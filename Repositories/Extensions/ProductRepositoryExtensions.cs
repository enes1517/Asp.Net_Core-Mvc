using Entities.Models;

namespace StoreApp.Infrastructe.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products,int? categoryId)
        {
            if (categoryId is null)
                return products;
            else
                return products.Where(prd=>prd.CategoryId.Equals(categoryId));

        }
        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string searchTerm)
        {
            if (String.IsNullOrWhiteSpace(searchTerm))
               return products;
            else
                return products.Where(prd=>prd.ProductName.ToLower().Contains(searchTerm.ToLower()));
        }
        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int MinPrice,int MaxPrice,bool ValidPrice)
        {
            if(ValidPrice)
            {
                return products.Where(prd=>prd.Price>=MinPrice&&prd.Price<=MaxPrice);
            }
            else
                return products;
        }
        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products,int PageNumber,int PageSize )
        {
            return products.Skip((PageNumber-1)*PageSize)
                .Take(PageSize);
        }

    }
}
