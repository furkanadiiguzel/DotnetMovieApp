using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Index("movieid", "genreid", Name = "moviegenre_movieid_genreid_key", IsUnique = true)]
public partial class moviegenre
{
    [Key]
    public int id { get; set; }

    public int? movieid { get; set; }

    public int? genreid { get; set; }

    [ForeignKey("genreid")]
    [InverseProperty("moviegenre")]
    public virtual genre? genre { get; set; }

    [ForeignKey("movieid")]
    [InverseProperty("moviegenre")]
    public virtual movie? movie { get; set; }
}