using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

internal class SampleRepository : ISampleRepository
{
    private SampleDbContext _dbContext;

    public SampleRepository(SampleDbContext sampleDbContext)
    {
        _dbContext = sampleDbContext;
    }

    public Customer AddCustomer(Customer customer)
    {
        _dbContext.Customers.Add(customer);

        _dbContext.SaveChanges();

        return customer;
    }

    public void DeleteCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public Customer? GetCustomer(int id)
    {
        return _dbContext.Customers.FirstOrDefault(o => o.Id == id);
    }

    public List<Customer> GetCustomers()
    {
        return _dbContext.Customers.ToList();
    }
}

