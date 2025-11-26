using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Entities;

public partial class recepie
{
    [Key]
    public int id { get; set; }

    public string recepie_name { get; set; } = null!;

    public string directions { get; set; } = null!;

    [Column(TypeName = "image")]
    public byte[]? image { get; set; }

    [ForeignKey("creatorid")]
    [InverseProperty("recepies")]
    public int creatorid { get; set; }
    // false god - never returns anythign but null
    public virtual user creator { get; set; } = null!;
}
