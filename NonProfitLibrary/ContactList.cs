using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitLibrary;

public class ContactList
{
    [Key]
    public int AccountNo { get; set; }
    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [StringLength(100)]
    public string? Street { get; set; }
    [StringLength(50)]
    public string? City { get; set; }
    [StringLength(6)]
    public string? PostalCode { get; set; }
    [StringLength(50)]
    public string? Country { get; set; }

    [ScaffoldColumn(false)] // This prevents the Created property from being scaffolded
    public DateTime Created { get; set; } = DateTime.Now;

    [ScaffoldColumn(false)] // This prevents the Modified property from being scaffolded
    public DateTime Modified { get; set; } = DateTime.Now;

    [ScaffoldColumn(false)] // This prevents the CreatedBy property from being scaffolded
    public string? CreatedBy { get; set; }

    [ScaffoldColumn(false)] // This prevents the ModifiedBy property from being scaffolded
    public string? ModifiedBy { get; set; }
}
