using Homework3.Entities;

namespace Homework3.Repositories.Abstracts;

public interface IProductRepository
{
    List<Product> GetAll();
    Product GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}
