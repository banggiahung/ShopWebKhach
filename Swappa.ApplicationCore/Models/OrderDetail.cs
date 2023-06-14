using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swappa.ApplicationCore.Models;

public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int Order_Id { get; set; }
    public string? Product_Name { get; set; }
    public decimal Product_Price { get; set; }
    public string FormattedPrice
        {
            get { return String.Format("{0:C}", Product_Price); }
        }
    public int? Product_Quantity { get; set; }
    public int ProductId { get; set; }
    public string? Product_Image { get; set; }
    public int OrderId { get; set; }

    public Order? Order { get; set; }
    public Product? product { get; set; }
    [NotMapped]
    public List<int> OrderDetailIds { get; set; }
    [NotMapped]
    public List<Order> RelatedOrders { get; set; }
    [NotMapped]

    public List<int> OrderIds { get; set; }
}