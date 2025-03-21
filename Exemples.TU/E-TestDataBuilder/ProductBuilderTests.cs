using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemples.TU.E_TestDataBuilder;

// Premièrement, définissons une classe Product que nous voulons tester
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedDate { get; set; }
}

// Maintenant, créons notre Test Data Builder pour Product
public class ProductBuilder
{
    private readonly Product _product;

    public ProductBuilder()
    {
        // Valeurs par défaut sensibles
        _product = new Product
        {
            Id = 1,
            Name = "Default Product",
            Price = 9.99m,
            Category = "Default Category",
            IsAvailable = true,
            CreatedDate = DateTime.Now
        };
    }

    // Méthodes fluides pour modifier les propriétés
    public ProductBuilder WithId(int id)
    {
        _product.Id = id;
        return this;
    }

    public ProductBuilder WithName(string name)
    {
        _product.Name = name;
        return this;
    }

    public ProductBuilder WithPrice(decimal price)
    {
        _product.Price = price;
        return this;
    }

    public ProductBuilder WithCategory(string category)
    {
        _product.Category = category;
        return this;
    }

    public ProductBuilder IsNotAvailable()
    {
        _product.IsAvailable = false;
        return this;
    }

    public ProductBuilder WithCreatedDate(DateTime createdDate)
    {
        _product.CreatedDate = createdDate;
        return this;
    }

    // Méthode pour construire le produit final
    public Product Build()
    {
        return _product;
    }

    // Méthodes pour créer des produits avec des configurations spécifiques
    public static Product CreateDefaultProduct()
    {
        return new ProductBuilder().Build();
    }

    public static Product CreateUnavailableProduct()
    {
        return new ProductBuilder()
            .IsNotAvailable()
            .Build();
    }

    public static Product CreateExpensiveProduct()
    {
        return new ProductBuilder()
            .WithPrice(999.99m)
            .WithCategory("Premium")
            .Build();
    }
}

// Maintenant, créons un service à tester
public class ProductService
{
    public bool IsProductPremium(Product product)
    {
        return product.Price > 100 && product.Category == "Premium";
    }

    public bool CanPurchase(Product product)
    {
        return product.IsAvailable && product.Price > 0;
    }
}

// Enfin, écrivons quelques tests xUnit qui utilisent notre Builder
public class ProductServiceTests
{
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productService = new ProductService();
    }

    [Fact]
    public void IsProductPremium_WithExpensiveProduct_ReturnsTrue()
    {
        // Arrange
        var expensiveProduct = new ProductBuilder()
            .WithPrice(150.00m)
            .WithCategory("Premium")
            .Build();

        // Act
        var result = _productService.IsProductPremium(expensiveProduct);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsProductPremium_WithCheapProduct_ReturnsFalse()
    {
        // Arrange
        var cheapProduct = new ProductBuilder()
            .WithPrice(50.00m)
            .WithCategory("Premium")
            .Build();

        // Act
        var result = _productService.IsProductPremium(cheapProduct);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPurchase_WithUnavailableProduct_ReturnsFalse()
    {
        // Arrange
        var unavailableProduct = new ProductBuilder()
            .IsNotAvailable()
            .Build();

        // Act
        var result = _productService.CanPurchase(unavailableProduct);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPurchase_WithDefaultProduct_ReturnsTrue()
    {
        // Arrange 
        var defaultProduct = ProductBuilder.CreateDefaultProduct();

        // Act
        var result = _productService.CanPurchase(defaultProduct);

        // Assert
        Assert.True(result);
    }
}