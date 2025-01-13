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
        public async Task<IActionResult> Index()
        {
            // Pobierz wszystkie zamówienia
            var orders = await _orderService.GetAllOrdersAsync();

            // Mapowanie zamówień na DTO (jeśli chcesz używać DTO)
            var orderListDto = orders.Select(order => new OrderListDto
            {
                OrderId = order.OrderId,
                Date = order.Date,
                OrderStatus = order.OrderStatus,
                FullPrice = order.FullPrice,
                ShippingMethod = order.Shipping.Name, // Assuming Shipping has a Name property
                PaymentMethod = order.Payment.Name  // Assuming Payment is an Enum
            }).ToList();

            return View(orderListDto);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound(); // Jeśli zamówienia nie znaleziono, zwróć 404
            }

            // Mapowanie zamówienia na DTO
            var orderDto = new ManageOrderDto
            {
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus
            };

            return View(orderDto); // Przekazanie danych do widoku `Manage`
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, StatusEnum orderStatus)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Zmiana statusu zamówienia
            order.OrderStatus = orderStatus;
            await _orderService.UpdateOrderAsync(order);

            TempData["Message"] = "Status zamówienia został zmieniony.";
            return RedirectToAction("Index");
        }

    }
}
