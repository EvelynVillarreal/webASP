using MongoDB.Entities;
using ProductosMVC.Models;

namespace ProductosMVC.Services;

public sealed class ProductService : IProductService
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await DB.Find<Product>()
            .Sort(p => p.RegistrationDate, Order.Descending)
            .ExecuteAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await DB.Find<Product>().OneAsync(id);
    }

    public async Task CreateProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        product.RegistrationDate = DateTime.UtcNow;
        await product.SaveAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var existingProduct = await DB.Find<Product>().OneAsync(product.ID);

        if (existingProduct is null)
        {
            throw new InvalidOperationException($"Product with Id {product.ID} was not found");
        }

        product.RegistrationDate = existingProduct.RegistrationDate;
        await product.SaveAsync();
    }

    public async Task DeleteProductAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        await DB.DeleteAsync<Product>(id);
    }
}
