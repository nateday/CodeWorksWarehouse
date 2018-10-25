using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeWorksWarehouse.Common.Entities;
using CodeWorksWarehouse.Web.Models;
using CodeWorksWarehouse.Business.Services;

namespace CodeWorksWarehouse.Web.Controllers
{
    public class OrdersViewController : Controller
    {
        OrderService _service;

        public OrdersViewController(OrderService ordersService)
        {
            _service = ordersService;
        }

        // GET: OrdersView
        public IActionResult Index()
        {
            return View(_service.GetUnprocessedOrders());
        }

        // GET: OrdersView/Details/5
        public IActionResult Details(Guid id)
        {

            var order = _service.GetOrder(id);

            return View("Details", order);
        }

        // GET: OrdersView/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: OrdersView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            _service.CreateOrder(order);

            return View("Details", order);
        }

        // GET: OrdersView/Edit/5
        public IActionResult Edit(Guid id)
        {
            var order = _service.GetOrder(id);

            return View("Edit", order);
        }

        // POST: OrdersView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,ProductId,CreatedAt,ProcessedAt,RemoveStock,Stock")] Order order)
        {

            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid order.");
            }

            return View("Edit", order);
        }

        // POST: OrdersView/ProcessOrder/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessOrder([Bind("Id,ProductId,CreatedAt,ProcessedAt,RemoveStock,Stock")] Guid id)
        {

            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid order.");
            }

            _service.ProcessOrder(id);

            return View("Index", _service.GetUnprocessedOrders());
        }

        // GET: OrdersView/Delete/5
        public IActionResult Delete(Guid id)
        {

            var order = _service.GetOrder(id);

            return View("Delete", order);
        }

        //// POST: OrdersView/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(Guid id)
        //{
        //    var order = _service.DeleteOrder(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
