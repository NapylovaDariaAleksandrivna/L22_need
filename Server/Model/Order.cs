namespace Server;
using System;
using System.ComponentModel.DataAnnotations;
public class Order
{
    [StringLength(32, MinimumLength = 32)]
    public string ID { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }
    [Range(0.01, 10000)]
    public double Price { get; set; }
    public Order(string id, string name, double price)
    {
        ID = id;
        Name = name;
        Price = price;
    }
}
