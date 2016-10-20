using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext DbContext { get; } = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return DbContext.Products; }
        }
    }
}
