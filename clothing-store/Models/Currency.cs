using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Currency
{
    [Key]
    public int CurrencyId { get; set; }  // Identyfikator waluty w bazie danych

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }  // Nazwa waluty (np. Dolar amerykański)

    [Required]
    [MaxLength(10)]
    public string Code { get; set; }  // Kod waluty (np. USD, EUR)

    [Column(TypeName = "decimal(18,4)")]
    public decimal Rate { get; set; }  // Kurs wymiany na PLN

    public DateTime LastUpdated { get; set; }  // Data ostatniej aktualizacji kursu
}

