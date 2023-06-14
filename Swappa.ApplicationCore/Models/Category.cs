using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swappa.ApplicationCore.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? Category_Name { get; set; }
    public string? Category_Image { get; set; }
    public ICollection<SubCategory>? SubCategories { get; set; }
    [NotMapped]
    public IFormFile? CatePath { get; set; }
}