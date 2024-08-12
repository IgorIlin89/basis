using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Exceptions;
namespace ApiCouponProduct.Database;

internal class ProductRepository : IProductRepository
{
    private readonly ApiCouponProductContext _context;

    public ProductRepository(ApiCouponProductContext context)
    {
        _context = context;
    }

    public Product AddProduct(Product product)
    {
        var existingProduct = _context.Product.FirstOrDefault(o => o.Name == product.Name &&
        o.Producer == product.Producer);

        if (existingProduct is not null)
        {
            throw new ProductExistsException($"A product with the name '{product.Name}' and the producer " +
                $"{product.Producer}' allready exists");
        }

        var response = _context.Product.Add(product);

        return response.Entity;
    }

    public void Delete(int id)
    {
        var user = _context.Product.FirstOrDefault(o => o.Id == id);

        if (user is not null)
        {
            _context.Remove(user);
        }
    }

    public Product GetProductById(int id)
    {
        var product = _context.Product.FirstOrDefault(o => o.Id == id);

        if (product is null)
        {
            throw new NotFoundException($"Product with the id '{id}' does not exist");
        }

        return product;
    }

    public List<Product> GetProductList()
    {
        return _context.Product.ToList();
    }

    public Product Update(Product product)
    {
        var productToEdit = _context.Product.FirstOrDefault(o => o.Id == product.Id);

        if (productToEdit is null)
        {
            throw new NotFoundException($"Product with the Id '{product.Id}' does not exist and could not be updated");
        }

        productToEdit.Name = product.Name;
        productToEdit.Producer = product.Producer;
        productToEdit.Category = product.Category;
        productToEdit.Picture = product.Picture;
        productToEdit.Price = product.Price;

        return productToEdit;
    }
}
