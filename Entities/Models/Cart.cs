using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; }

        public Cart()
        {
            Lines = new List<CartLine>();

        }
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines.Where(l => l.Product.ProductId.Equals(product.ProductId)).SingleOrDefault();
            if (line is null)
                Lines.Add(line = new CartLine() { Product = product, Quantity = quantity });

            else
            {
               line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product Product)=>Lines.RemoveAll(L=>L.Product.ProductId.Equals(Product.ProductId));
        public  decimal ComputeTotalValue()=>Lines.Sum(e=>e.Quantity*e.Product.Price);
        public virtual void Clear()=>Lines.Clear();
        
    }
}
