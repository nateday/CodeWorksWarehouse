using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeWorksWarehouse.Web.Models;
using CodeWorksWarehouse.Common.Interface;
using CodeWorksWarehouse.Business.Services;
using CodeWorksWarehouse.Common.Entities;

namespace CodeWorksWarehouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetUnprocessedOrders()
        {
            //No need for IActionResult/returning Ok. Can just return GetUnprocessedOrders (A list).
            return Ok(_orderService.GetUnprocessedOrders());
        }

        [HttpGet("{productId}")]
        public IActionResult GetOrders(Guid productId)
        {
            return Ok(_orderService.GetOrders(productId));
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(Guid id)
        {
            return Ok(_orderService.GetOrder(id));
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost("create")]
        public IActionResult Create([FromForm] Order order)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid order.");
            }

            return Ok(_orderService.CreateOrder(order));
        }

        [HttpPut("update")]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid order.");
            }

            _orderService.UpdateOrder(order);
            return Ok();
        }
    }
}
