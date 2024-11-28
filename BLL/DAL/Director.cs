using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class Director
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    [Required]
    public string name { get; set; } = null!;

    [StringLength(255)]
    [Required]
    public string surname { get; set; } = null!;

    public bool isretired { get; set; }

    [InverseProperty("director")]
    public virtual ICollection<movie> movie { get; set; } = new List<movie>();
}