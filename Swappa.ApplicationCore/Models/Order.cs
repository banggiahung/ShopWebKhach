using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swappa.ApplicationCore.Models;

public class Order
{
    public int OrderId { get; set; }
    public string? Customer_Id { get; set; }
    public string? Customer_Name { get; set; }
    public string? Phone_Number { get; set; }
    public string? Address { get; set; }
    public DateTime CreateDate { get; set; }
    public bool? Order_Status { get; set; }
    public decimal? Total_Price { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    [NotMapped]
    public int? ProductIds { get; set; }
    [NotMapped]

    public int? QuantityClient { get; set; }

}