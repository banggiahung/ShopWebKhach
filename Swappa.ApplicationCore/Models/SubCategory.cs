using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swappa.ApplicationCore.Models;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string? SubCategory_Name { get; set; }
    public Category? Category { get; set; }
    public ICollection<Product>? Product { get; set; }
   
}