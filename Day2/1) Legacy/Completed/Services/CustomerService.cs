using CustomerManager.Models;

namespace CustomerManager.Services;

public interface ICustomerService
{
    Customer? GetCustomer(int id);
    Customer? SearchCustomer(string name);
    List<Customer> GetAllCustomers();
}

public class CustomerService : ICustomerService
{
    private static readonly List<Customer> Customers = new()
    {
        new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", CreatedAt = DateTime.Now },
        new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", CreatedAt = DateTime.Now },
        new Customer { Id = 3, Name = "Bob Wilson", Email = "bob@example.com", CreatedAt = DateTime.Now }
    };

    public Customer? GetCustomer(int id)
    {
        return Customers.FirstOrDefault(c => c.Id == id);
    }

    public Customer? SearchCustomer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        return Customers.FirstOrDefault(c => 
            c.Name != null && c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Customer> GetAllCustomers()
    {
        return Customers;
    }
}
