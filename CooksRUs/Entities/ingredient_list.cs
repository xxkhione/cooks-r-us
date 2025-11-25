using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Entities;

public partial class ingredient_list
{
    [Key]
    public int id { get; set; }

    public int recepie_id { get; set; }

    [InverseProperty("list")]
    public virtual ICollection<ingredient> ingredients { get; set; } = new List<ingredient>();
}
