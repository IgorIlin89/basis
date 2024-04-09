using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

public interface ISampleRepository
{
    List<Customer> GetCustomers();

    Customer? GetCustomer(int id);

    Customer AddCustomer(Customer customer);

    void DeleteCustomer(int id);
}