using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swappa.ApplicationCore.Models;

public class Product
{
    public int ProductId { get; set; }
    public int Product_Number { get; set; }
    public string? Product_Name { get; set; }
    public string? Product_Description { get; set; }
    public decimal Product_Price { get; set; }
    public string? Product_Status { get; set; }
    public int Product_Quantity { get; set; }
    public int SubCategoryId { get; set; }
    public string FormattedPrice
        {
            get { return String.Format("{0:C}", Product_Price); }
        }
    public SubCategory? SubCategory { get; set; }
    public string? Product_ImagePath { get; set; }
    [NotMapped]
    public IFormFile? PrPath { get; set; }
    [NotMapped]
    public string? NameSubCategory { get; set; }
}