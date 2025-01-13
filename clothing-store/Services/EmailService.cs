using clothing_store.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Xml.Linq;
namespace clothing_store.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendConfirmationEmail(string name)
        {
            var apiKey = _configuration.GetSection("EmailApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("starsentstore@gmail.com", "StarSent");
            var subject = "Account at StarSent";
            var to = new EmailAddress("paweldomalewski02@gmail.com", "domal");
            var templateId = "d-5da83691206a4e788dcfafe7af910429";
            var templateData = new { Name = name};
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, templateData);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
        }
        public async Task SendForgotPasswordEmail(string name, string url)
        {
            var apiKey = _configuration.GetSection("EmailApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("starsentstore@gmail.com", "StarSent");
            var subject = "Your password reset link";
            var to = new EmailAddress("paweldomalewski02@gmail.com", "domal");
            var templateId = "d-d6945322db8d44eb938196b8793f28a6";
            var templateData = new { Link = url };
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, templateData);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
        }
        public async Task SendOrderConfirmationEmail(Order order)
        {
            var apiKey = _configuration.GetSection("EmailApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("starsentstore@gmail.com", "StarSent");
            var subject = $"Order: {order.OrderId} Confirmation";
            var to = new EmailAddress("paweldomalewski02@gmail.com", "domal");
            var templateId = "d-7ca7771e65a643edacd5ae64e796e70d";

            // Pobranie szczegółów zamówienia, w tym cena jednostkowa i łączna cena dla każdego produktu
            var orderProducts = order.Products.Select(op => new {
                ProductName = op.Product.Name,
                Quantity = op.Quantity,
                Price = op.Product.Price,
                TotalPrice = op.Quantity * op.Product.Price // Obliczenie łącznej ceny dla każdego produktu
            }).ToList();

            // Budowanie treści zamówionych produktów (w tym ceny jednostkowej i łącznej)
            var fullinfo = string.Join("<br />", orderProducts.Select(op =>
                $"{op.ProductName} x{op.Quantity} - {op.Price:C} each, Total: {op.TotalPrice:C}"
            ));

            var templateData = new
            {
                OrderId = order.OrderId,
                info = fullinfo,  // Wstawienie danych o produktach do treści e-maila
                FullPrice = order.FullPrice // Pełna cena zamówienia
            };

            // Tworzenie wiadomości e-mail
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, templateData);

            // Wysłanie e-maila
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
        }
    }
}
