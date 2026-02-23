using CustomerManager.Models;
using CustomerManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Customer Manager API",
        Version = "2.0.0",
        Description = "Minimal API로 현대화된 고객 관리 시스템"
    });
});

builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ============================================
// Minimal API Endpoints
// ============================================

// Health Check 엔드포인트
app.MapGet("/health", () =>
{
    var response = new HealthResponse
    {
        Status = "Healthy",
        Message = "Customer Manager API is running (Minimal API)",
        Timestamp = DateTime.UtcNow
    };
    return Results.Ok(response);
})
.WithName("HealthCheck")
.WithTags("Health")
.Produces<HealthResponse>(StatusCodes.Status200OK);

// 고객 검색 엔드포인트 (이름으로 검색)
app.MapGet("/api/customers/search", (string? name, ICustomerService customerService) =>
{
    if (string.IsNullOrWhiteSpace(name))
    {
        return Results.BadRequest(new { error = "Customer name is required" });
    }

    var customer = customerService.SearchCustomer(name);
    if (customer == null)
    {
        return Results.NotFound(new { error = $"Customer '{name}' not found" });
    }

    return Results.Ok(customer);
})
.WithName("SearchCustomer")
.WithTags("Customers")
.Produces<Customer>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

// 고객 조회 엔드포인트 (ID로 조회)
app.MapGet("/api/customers/{id:int}", (int id, ICustomerService customerService) =>
{
    if (id <= 0)
    {
        return Results.BadRequest(new { error = "Invalid customer ID" });
    }

    var customer = customerService.GetCustomer(id);
    if (customer == null)
    {
        return Results.NotFound(new { error = $"Customer with ID {id} not found" });
    }

    return Results.Ok(customer);
})
.WithName("GetCustomer")
.WithTags("Customers")
.Produces<Customer>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status404NotFound);

// 전체 고객 목록 조회
app.MapGet("/api/customers", (ICustomerService customerService) =>
{
    var customers = customerService.GetAllCustomers();
    return Results.Ok(customers);
})
.WithName("GetAllCustomers")
.WithTags("Customers")
.Produces<List<Customer>>(StatusCodes.Status200OK);

app.Run();
