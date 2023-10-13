using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitLibrary;

public class TransactionType
{
    public int TransactionTypeId { get; set; }
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    [StringLength(100)]
    public string? Description { get; set; }

    [ScaffoldColumn(false)] // This prevents the Created property from being scaffolded
    public DateTime Created { get; set; } = DateTime.Now;

    [ScaffoldColumn(false)] // This prevents the Created property from being scaffolded
    public DateTime Modified { get; set; } = DateTime.Now;

    [ScaffoldColumn(false)] // This prevents the Created property from being scaffolded
    public string? CreatedBy { get; set; }

    [ScaffoldColumn(false)] // This prevents the Created property from being scaffolded
    public string? ModifiedBy { get; set; }
}
