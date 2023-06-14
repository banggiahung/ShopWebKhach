using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Swappa.ApplicationCore.Models;

public class ApplicationUser : IdentityUser
{

    public string? FullName { get; set; }
    public string? Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
    //public ICollection<ShoppingCart>? CartItems { get; set; }
}