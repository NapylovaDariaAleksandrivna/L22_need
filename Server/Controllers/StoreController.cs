namespace Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public StoreController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    [HttpPost]
    [Route("/store/add")]
    public IActionResult Add([FromBody] Order newOrder)
    {
        _orderRepository.AddOrder(newOrder);
        return Ok(_orderRepository.GetAllOrders());
    }
    [HttpDelete]
    [Route("/store/remove")]
    public IActionResult Delete(string id)
    {
        try
        {
            _orderRepository.DeleteOrder(id);
            return Ok($"{id} удален");
        }
        catch
        {
            return NotFound($"{id} не найден");
        }
    }
    [HttpGet]
    [Route("/store/show")]
    public IActionResult Show()
    {
        return Ok(_orderRepository.GetAllOrders());
    }
}