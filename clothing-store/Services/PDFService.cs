using clothing_store.Interfaces;
using clothing_store.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System.Collections.Generic;

namespace clothing_store.Services
{
    public class PDFService : IPDFService
    {
        private readonly IProductService _productService;

        public PDFService(IProductService productService)
        {
            _productService = productService;
        }

        public byte[] GenerateProductPriceListPdf(List<Product> products)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Border(1)
                        .Padding(5)
                        .Text("Product Price List")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium)
                        .AlignCenter();

                    page.Content()
                        .Table(table =>
                        {
                            // Define columns
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Product Name
                                columns.RelativeColumn(4); // Net Price
                                columns.RelativeColumn(2); // VAT
                                columns.RelativeColumn(2); // Gross Price
                                columns.RelativeColumn(2); // Stock
                                columns.RelativeColumn(2); // Description
                            });

                            // Add table header with borders and padding
                            table.Header(header =>
                            {
                                header.Cell().Border(1).Background(Colors.Orange.Medium).Padding(5).Text("Name").Bold().AlignCenter();
                                header.Cell().Border(1).Background(Colors.Orange.Medium).Padding(5).Text("Description").Bold().AlignCenter();
                                header.Cell().Border(1).Background(Colors.Orange.Medium).Padding(5).Text("Net Price [zł]").Bold().AlignCenter();
                                header.Cell().Border(1).Background(Colors.Orange.Medium).Padding(5).Text("VAT (%)").Bold().AlignCenter();
                                header.Cell().Border(1).Background(Colors.Orange.Medium).Padding(5).Text("Gross Price [zł]").Bold().AlignCenter();
                                header.Cell().Border(1).Background(Colors.Orange.Medium).Padding(5).Text("Stock").Bold().AlignCenter();
                            });

                            // Add product rows with borders and padding
                            // Add product rows with borders and padding
                            foreach (var product in products)
                            {
                                var vatRate = product.Tax?.Value ?? 0;
                                decimal vatRateDecimal = (decimal)vatRate;
                                decimal nettoPrice = Math.Round(product.Price / (1 + vatRateDecimal / 100), 2, MidpointRounding.AwayFromZero);

                                // Stock cell logic
                                var stockText = product.Quantity > 0 ? product.Quantity.ToString() : "Out of stock";

                                // Define stock background color based on product quantity (hex string values)
                                var stockBackgroundColor = product.Quantity == 0
                                    ? Colors.Red.Medium // Light red for out of stock
                                    : product.Quantity < 10
                                    ? Colors.Yellow.Medium // Light yellow for less than 10
                                    : Colors.Green.Medium; // Light green for more than 10

                                table.Cell().Border(1).Padding(5).Text(product.Name).AlignCenter();
                                table.Cell().Border(1).Padding(5).Text(product.Description ?? "-").AlignCenter();
                                table.Cell().Border(1).Padding(5).Text($"{product.Price}").AlignCenter();
                                table.Cell().Border(1).Padding(5).Text($"{vatRate:F2}").AlignCenter();
                                table.Cell().Border(1).Padding(5).Text($"{nettoPrice}").AlignCenter();

                                // Apply background color to the stock cell without changing the text color
                                table.Cell().Border(1)
                                    .Background(stockBackgroundColor)
                                    .Padding(5)// Set background color
                                    .Text(stockText)
                                    .AlignCenter(); // Stock text

                            }


                        });

                    page.Footer()
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span("Generated on: ");
                            text.Span($"{DateTime.Now:yyyy-MM-dd HH:mm}").SemiBold();
                        });
                });
            }).GeneratePdf();
        }
    }
}
