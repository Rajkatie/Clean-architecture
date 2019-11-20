using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class InMemoryProductRepository : IProductRepository
    {
        public Product GetById(int id)
        {
            return new Product {Id = id, Name = "toys"};
        }
    }
}