using MobyLabWebProgramming.Backend.Entities;
using MobyLabWebProgramming.Infrastructure.Database;

namespace MobyLabWebProgramming.Backend;

public static class DbInitializer
{
    public static void Initialize(WebAppDatabaseContext context)
    {
        if (context.Products.Any()) return;

        var products = new List<Product>
        {
            new Product
            {
                Name = "Sapca F1 Mercedes",
                Description = "Sapca oficiala Mercedes F1 Team 2021",
                Price = 100,
                Type = "Sapca",
                Brand = "Mercedes",
                Stock = 10
            },
            new Product
            {
                Name = "Sapca F1 Ferrari",
                Description = "Sapca oficiala Ferrari F1 Team 2021",
                Price = 200,
                Type = "Sapca",
                Brand = "Ferrari",
                Stock = 20
            },
            new Product
            {
                Name = "Sapca F1 Red Bull",
                Description = "Sapca oficiala Red Bull Racing F1 Team 2021",
                Price = 300,
                Type = "Sapca",
                Brand = "Red Bull",
                Stock = 30
            },
            new Product()
            {
                Name = "Tricou F1 Mercedes",
                Description = "Tricou oficial Mercedes F1 Team 2021",
                Price = 100,
                Type = "Tricou",
                Brand = "Mercedes",
                Stock = 10
            },
            new Product()
            {
                Name = "Tricou F1 Ferrari",
                Description = "Tricou oficial Ferrari F1 Team 2021",
                Price = 200,
                Type = "Tricou",
                Brand = "Ferrari",
                Stock = 20
            },
            new Product()
            {
                Name = "Tricou F1 Red Bull",
                Description = "Tricou oficial Red Bull Racing F1 Team 2021",
                Price = 300,
                Type = "Tricou",
                Brand = "Red Bull",
                Stock = 30
            },
            new Product()
            {
                Name = "Palarie F1 Mercedes",
                Description = "Palarie oficiala Mercedes F1 Team 2021",
                Price = 100,
                Type = "Palarie",
                Brand = "Mercedes",
                Stock = 10
            },
            new Product()
            {
                Name = "Palarie F1 Ferrari",
                Description = "Palarie oficiala Ferrari F1 Team 2021",
                Price = 200,
                Type = "Palarie",
                Brand = "Ferrari",
                Stock = 20
            },
            new Product()
            {
                Name = "Palarie F1 Red Bull",
                Description = "Palarie oficiala Red Bull Racing F1 Team 2021",
                Price = 300,
                Type = "Palarie",
                Brand = "Red Bull",
                Stock = 30
            },
            new Product()
            {
                Name = "Jacheta F1 Mercedes",
                Description = "Jacheta oficiala Mercedes F1 Team 2021",
                Price = 100,
                Type = "Jacheta",
                Brand = "Mercedes",
                Stock = 10
            },
            new Product()
            {
                Name = "Jacheta F1 Ferrari",
                Description = "Jacheta oficiala Ferrari F1 Team 2021",
                Price = 200,
                Type = "Jacheta",
                Brand = "Ferrari",
                Stock = 20
            },
            new Product()
            {
                Name = "Jacheta F1 Red Bull",
                Description = "Jacheta oficiala Red Bull Racing F1 Team 2021",
                Price = 300,
                Type = "Jacheta",
                Brand = "Red Bull",
                Stock = 30
            }
            
        };
        
        foreach (var product in products)
        {
            context.Products.Add(product);
        }
        
        context.SaveChanges();

    }
}