using Backend.Api.Models;

namespace Backend.Api.Data;

public sealed class InMemoryProductCatalog
{
    private readonly List<Product> _product = new()
    {
        new Product("P001", "Kaos Polos Cotton Combed", 75000m, 150, "Atasan"),
        new Product("P002", "Baju Distro Grafis", 150000m, 100, "Atasan"),
        new Product("P003", "Kemeja Flanel Kotak", 220000m, 75, "Atasan"),
        new Product("P004", "Jaket Hoodie Polos", 250000m, 60, "Outerwear"),
        new Product("P005", "Sweater Crewneck", 185000m, 80, "Outerwear"),
        new Product("P006", "Celana Chino Panjang", 275000m, 90, "Bawahan"),
        new Product("P007", "Celana Jeans Denim", 350000m, 110, "Bawahan"),
        new Product("P008", "Celana Pendek Cargo", 135000m, 65, "Bawahan"),
        new Product("P009", "Kemeja Batik Modern", 195000m, 45, "Formal"),
        new Product("P010", "Blouse Wanita Casual", 125000m, 85, "Atasan Wanita"),
        new Product("P011", "Rok Plisket Panjang", 140000m, 50, "Bawahan Wanita"),
        new Product("P012", "Dress Casual Mewah", 320000m, 30, "Terusan"),
        new Product("P013", "Jaket Denim Trucker", 380000m, 40, "Outerwear"),
        new Product("P014", "Kaos Polo Bordir", 115000m, 120, "Atasan"),
        new Product("P015", "Cardigan Rajut", 165000m, 70, "Outerwear"),
        new Product("P016", "Celana Training Olahraga", 145000m, 95, "Sportswear"),
        new Product("P017", "Kaos Jersey Dryfit", 95000m, 130, "Sportswear"),
        new Product("P018", "Kemeja Formal Putih", 180000m, 100, "Formal"),
        new Product("P019", "Jaket Bomber Waterproof", 299000m, 55, "Outerwear"),
        new Product("P020", "Tote Bag Kanvas Distro", 65000m, 200, "Aksesoris")

    };

    public IReadOnlyList<Product> GetAll()
    {
        return _product.ToArray();
    }

    public Product? GetByCode(string productCode)
    {
        return _product.FirstOrDefault(product => string.Equals(product.ProductCode,productCode.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public void Add(Product product)
    {
        if (_product.Any(p => p.ProductCode == product.ProductCode))
        {
            throw new ArgumentException("product code already exists.");
        }

        _product.Add(product);
    }

    public void Remove(string productCode)
    {
        var product = GetByCode(productCode);

        if(product != null)
        {   
        _product.Remove(product);
        }
    }

    public void Put(string productCode, Product updateProduct)
    {
        var product = GetByCode(productCode) ?? throw new ArgumentException("product is not found");

        product.ProductCode = updateProduct.ProductCode;
        product.Name = updateProduct.Name;
        product.Price = updateProduct.Price;
        product.Stock = updateProduct.Stock;        
        product.Category = updateProduct.Category;        

    }
}