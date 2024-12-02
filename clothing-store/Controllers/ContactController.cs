using Microsoft.AspNetCore.Mvc;

namespace YourStore.Controllers
{
    public class ContactController : Controller
    {
        // Contact Selection Page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Store Contact Form
        [HttpGet]
        public IActionResult StoreContactForm()
        {
            return View();
        }

        // Product Contact Form
        [HttpGet]
        public IActionResult ProductContactForm()
        {
            return View();
        }

        // Handle Store Contact Form Submission
        [HttpPost]
        public IActionResult SubmitStoreContact(string name, string email, string subject, string message)
        {
            // Logic to handle store contact
            ViewBag.Message = "Thank you for contacting the store. We'll respond shortly!";
            return View("StoreContactForm");
        }

        // Handle Product Contact Form Submission
        [HttpPost]
        public IActionResult SubmitProductContact(string name, string email, string productId, string message)
        {
            // Logic to handle product contact
            ViewBag.Message = "Thank you for contacting us about the product. We'll respond shortly!";
            return View("ProductContactForm");
        }
    }
}
