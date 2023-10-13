using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitLibrary;

public class Donations
{
    [Key]
    public int TransId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int AccountNo { get; set; }
    [Required]
    public int TransactionTypeId { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public float Amount { get; set; }
    [Required]
    public int PaymentMethodId { get; set; }
    public string? Notes { get; set; }

    // [ScaffoldColumn(false)]
    public DateTime Created { get; set; } = DateTime.Now;

    // [ScaffoldColumn(false)]
    public DateTime Modified { get; set; } = DateTime.Now;

    // [ScaffoldColumn(false)]
    public required string CreatedBy { get; set; }

    // [ScaffoldColumn(false)]
    public required string ModifiedBy { get; set; }

    // Navigation properties
    [ForeignKey("AccountNo")]
    public ContactList? Account { get; set; }

    [ForeignKey("TransactionTypeId")]
    public TransactionType? TransactionType { get; set; }

    [ForeignKey("PaymentMethodId")]
    public PaymentMethod? PaymentMethod { get; set; }
}
