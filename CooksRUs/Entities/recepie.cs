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

    public int ingredient_list { get; set; }

    public string directions { get; set; } = null!;

    [Column(TypeName = "image")]
    public byte[]? image { get; set; }

    public int creator_id { get; set; }

    [ForeignKey("creator_id")]
    [InverseProperty("recepies")]
    public virtual user creator { get; set; } = null!;
}
