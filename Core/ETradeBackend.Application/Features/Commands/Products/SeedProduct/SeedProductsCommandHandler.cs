using ETradeBackend.Application.Repositories.Products;
using ETradeBackend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Application.Features.Commands.Products.SeedProduct;

public class SeedProductsCommandHandler(
    IProductWriteRepository productWriteRepository,
    IProductReadRepository productReadRepository) : IRequestHandler<SeedProductsCommandRequest, string>
{
    public async Task<string> Handle(SeedProductsCommandRequest request, CancellationToken cancellationToken)
    {
        // var hasAnyProduct = await productReadRepository.Table.AnyAsync(cancellationToken);
        // if(hasAnyProduct)
        //     return "Products already exist. Seeding is not necessary.";
        //
        // await productWriteRepository.AddRangeAsync(new List<Product>()
        // {
        //     new Product { Id = Guid.NewGuid(), Name = "Apple iPhone 14 128GB", Price = 32499.99m, Stock = 25 },
        //     new Product { Id = Guid.NewGuid(), Name = "Samsung Galaxy S23 256GB", Price = 28999.50m, Stock = 18 },
        //     new Product { Id = Guid.NewGuid(), Name = "Lenovo ThinkPad E14 Laptop", Price = 18999.00m, Stock = 12 },
        //     new Product { Id = Guid.NewGuid(), Name = "HP Pavilion Gaming Laptop", Price = 21450.75m, Stock = 9 },
        //     new Product { Id = Guid.NewGuid(), Name = "Dell Inspiron 15 3000", Price = 17200.40m, Stock = 14 },
        //     new Product { Id = Guid.NewGuid(), Name = "Logitech MX Master 3 Mouse", Price = 2499.99m, Stock = 60 },
        //     new Product { Id = Guid.NewGuid(), Name = "Razer DeathAdder V2 Mouse", Price = 1599.90m, Stock = 45 },
        //     new Product { Id = Guid.NewGuid(), Name = "Apple Magic Keyboard", Price = 3299.00m, Stock = 30 },
        //     new Product { Id = Guid.NewGuid(), Name = "SteelSeries Apex 7 Keyboard", Price = 2890.25m, Stock = 22 },
        //     new Product { Id = Guid.NewGuid(), Name = "Sony WH-1000XM5 Headphones", Price = 8499.99m, Stock = 16 },
        //     new Product { Id = Guid.NewGuid(), Name = "JBL Tune 510BT Headphones", Price = 1299.75m, Stock = 75 },
        //     new Product { Id = Guid.NewGuid(), Name = "Samsung 27'' Curved Monitor", Price = 7450.00m, Stock = 20 },
        //     new Product
        //     {
        //         Id = Guid.NewGuid(), Name = "LG UltraGear 24'' Gaming Monitor", Price = 6899.60m, Stock = 13
        //     },
        //     new Product { Id = Guid.NewGuid(), Name = "Asus TUF Gaming VG249Q Monitor", Price = 5999.90m, Stock = 11 },
        //     new Product { Id = Guid.NewGuid(), Name = "Kingston 16GB DDR4 RAM", Price = 1450.30m, Stock = 80 },
        //     new Product { Id = Guid.NewGuid(), Name = "Corsair Vengeance 32GB RAM", Price = 2899.99m, Stock = 55 },
        //     new Product { Id = Guid.NewGuid(), Name = "Samsung 1TB SSD NVMe", Price = 2150.00m, Stock = 40 },
        //     new Product { Id = Guid.NewGuid(), Name = "WD Blue 2TB HDD", Price = 1250.75m, Stock = 65 },
        //     new Product { Id = Guid.NewGuid(), Name = "Seagate Barracuda 1TB HDD", Price = 980.20m, Stock = 70 },
        //     new Product { Id = Guid.NewGuid(), Name = "TP-Link Archer C6 Router", Price = 899.99m, Stock = 33 },
        //     new Product { Id = Guid.NewGuid(), Name = "Xiaomi Mi Smart Band 7", Price = 799.50m, Stock = 120 },
        //     new Product { Id = Guid.NewGuid(), Name = "Apple Watch Series 8", Price = 15999.00m, Stock = 10 },
        //     new Product { Id = Guid.NewGuid(), Name = "Samsung Galaxy Watch 5", Price = 10999.99m, Stock = 14 },
        //     new Product { Id = Guid.NewGuid(), Name = "Philips Hue Smart Bulb", Price = 650.40m, Stock = 90 },
        //     new Product { Id = Guid.NewGuid(), Name = "Dyson V11 Vacuum Cleaner", Price = 12499.99m, Stock = 6 },
        //     new Product { Id = Guid.NewGuid(), Name = "Arçelik 9kg Washing Machine", Price = 18200.00m, Stock = 8 },
        //     new Product { Id = Guid.NewGuid(), Name = "Vestel 50'' Smart TV", Price = 13999.90m, Stock = 15 },
        //     new Product { Id = Guid.NewGuid(), Name = "Bosch Dishwasher Series 4", Price = 16750.60m, Stock = 7 },
        //     new Product { Id = Guid.NewGuid(), Name = "Nespresso Coffee Machine", Price = 5499.75m, Stock = 19 },
        //     new Product { Id = Guid.NewGuid(), Name = "Tefal Air Fryer XXL", Price = 3499.99m, Stock = 27 }
        // });
        //
        //
        // await productWriteRepository.SaveChangesAsync();
        // return "Products seeded successfully.";
        return string.Empty;
    }
}