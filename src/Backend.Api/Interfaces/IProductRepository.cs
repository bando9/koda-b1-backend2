using Backend.Api.Models;

namespace Backend.Api.Interfaces;

public interface IProductRepository
{
    IReadOnlyList<Product> GetAll();
    Product? GetByCode(string productCode);
    bool TryAdd(Product product);
    bool TryUpdate(Product product);
    bool TryDelete(string productCode);
}