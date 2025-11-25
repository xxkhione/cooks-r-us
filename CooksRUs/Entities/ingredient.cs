using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Entities;

public partial class ingredient
{
    [Key]
    public int id { get; set; }

    public int list_id { get; set; }

    [Unicode(false)]
    public string ingredient_name { get; set; } = null!;

    [StringLength(20)]
    public string? amount { get; set; }

    [ForeignKey("list_id")]
    [InverseProperty("ingredients")]
    public virtual ingredient_list list { get; set; } = null!;
}
