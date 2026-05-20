using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace ProductosMVC.Models;

public sealed class Product : Entity
{
    private const decimal VatRate = 0.15m;

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    [Display(Name = "Category")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Base price is required")]
    [Range(0.01, 9999999.99, ErrorMessage = "Price must be greater than 0")]
    [DataType(DataType.Currency)]
    [Display(Name = "Base Price")]
    public decimal BasePrice { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    [Display(Name = "Quantity")]
    public int Quantity { get; set; }

    [Display(Name = "Description")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Display(Name = "Registration Date")]
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    [Ignore]
    [Display(Name = "VAT (15%)")]
    public decimal Tax => CalculateTax();

    [Ignore]
    [Display(Name = "Price with VAT")]
    public decimal PriceWithTax => BasePrice + Tax;

    [Ignore]
    [Display(Name = "Total")]
    public decimal Total => PriceWithTax * Quantity;

    private decimal CalculateTax()
    {
        return BasePrice * VatRate;
    }
}
