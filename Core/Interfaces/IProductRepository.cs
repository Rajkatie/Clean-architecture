using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Product GetById(int id);
    }
}