using Microsoft.AspNetCore.Mvc;
using CustomerManager.Models;
using CustomerManager.Services;

namespace CustomerManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("search")]
    public ActionResult<Customer?> SearchCustomer([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Customer name is required");
        }

        // TODO: Service 호출해서 고객 조회
        var customer = _customerService.SearchCustomer(name);
        if (customer == null)
        {
            return NotFound($"Customer '{name}' not found");
        }

        return Ok(customer);
    }

    // TODO: Step 5) Agent Tool로 변환될 기능
    [HttpGet("{id}")]
    public ActionResult<Customer?> GetCustomer(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid customer ID");
        }

        var customer = _customerService.GetCustomer(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }
}
