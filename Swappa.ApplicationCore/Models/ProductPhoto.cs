using Microsoft.AspNetCore.Identity;

namespace Swappa.ApplicationCore.Models;

public class ProductPhoto
{
    public int ProductPhotoId { get; set; }
    public int Product_Id { get; set; }
    public string? SmallImage_Path { get; set; }
    public string? LargeImage_Path { get; set; }
    public Product? Product { get; set; }
}