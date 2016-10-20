using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        public List<CartLine> LineCollection { get; set; } = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            var line = LineCollection.FirstOrDefault(x => x.Product.ProductID == product.ProductID);
            if (line == null)
                LineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Product product) => LineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public decimal ComputeTotalValue() => LineCollection.Sum(s => s.Product.Price * s.Quantity);

        public void Clear() => LineCollection.Clear();

        public IEnumerable<CartLine> Lines  => LineCollection;
    }
}
