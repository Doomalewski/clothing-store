using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Pobierz zamówienie wraz z produktami oraz dodatkowymi informacjami
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound(); // Jeśli nie znaleziono zamówienia, zwróć 404
            }

            // Mapowanie zamówienia na DTO
            var orderDetailsDto = new OrderDetailsDto
            {
                OrderId = order.OrderId,
                Date = order.Date,
                OrderStatus = order.OrderStatus,
                FullPrice = order.FullPrice,
                ShippingMethod = order.Shipping.Name, // Assuming Shipping has a Name property
                PaymentMethod = order.Payment.Name, // Assuming Payment is an Enum
                Address = $"{order.Street}, {order.City}, {order.State}, {order.ZipCode}, {order.Country}",
                Products = order.Products.Select(p => new OrderProductDto
                {
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity,
                    Price = p.Product.Price
                }).ToList()
            };

            return View(orderDetailsDto);
        }
    }
}
