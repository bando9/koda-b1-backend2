using Backend.Api.DTOs;
using Backend.Api.Interfaces;
using Backend.Api.Models;

namespace Backend.Api.Services;

public sealed class ProductService : IProductService
{

    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository=repository;
    }

    public ProductResponse? Create(CreateProduct request)
    {
        var product = new Product(request.ProductCode, request.Name, request.Price, request.Stock, request.Category);

        return _repository.TryAdd(product) ? ProductResponse.FromModel(product) : null;
    }

    public bool? Delete(string productCode)
    {
        return _repository.TryDelete(productCode);
    }

    public IReadOnlyList<ProductResponse> GetAll()
    {
        return _repository.GetAll().Select(ProductResponse.FromModel).ToArray();
    }

    public ProductResponse? GetByCode(string productCode)
    {
        var product = _repository.GetByCode(productCode);
        return product is null ? null : ProductResponse.FromModel(product);
    }

    public ProductResponse? Update(string productCode, UpdateProductRequest request)
    {
        var product = new Product(productCode, request.Name, request.Price, request.Stock, request.Category);
        return _repository.TryUpdate(product) ? ProductResponse.FromModel(product) : null;
    }

    bool IProductService.Delete(string productCode)
    {
        return _repository.TryDelete(productCode);
    }
}