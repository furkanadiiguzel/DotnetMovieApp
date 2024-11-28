using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class movie
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    [Required]
    public string name { get; set; } = null!;
    
    [Required]
    public DateOnly? releasedate { get; set; }

    [Precision(18, 2)]
    public decimal? totalrevenue { get; set; }

    public int? directorid { get; set; }
    
    [ForeignKey("directorid")]
    [InverseProperty("movie")]
    public virtual Director? director { get; set; }

    [InverseProperty("movie")]
    public virtual ICollection<moviegenre> moviegenre { get; set; } = new List<moviegenre>();
}