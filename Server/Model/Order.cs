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
    [StringLength(32, MinimumLength = 32)]
    public string DopPole { get; set; }
    public DateTime Time {get;set;}
    public Order(string id, string name, string dopPole, DateTime time)
    {
        ID = id;
        Name = name;
        DopPole = dopPole;
        Time= time;
    }
}
