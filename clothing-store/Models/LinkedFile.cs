using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkedFile
{
    [Key]
    public int FileId { get; set; }

    [Required(ErrorMessage = "File path is required.")]
    [StringLength(500, ErrorMessage = "File path cannot be longer than 500 characters.")]
    public string Path { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string Description { get; set; } = string.Empty;
}

