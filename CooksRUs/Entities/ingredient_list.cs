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
    [Required]
    //set this, should know the ID of the recepie being made
    public int recepie_id { get; set; }

    [InverseProperty("list")]
    public virtual ICollection<ingredient> ingredients { get; set; } = new List<ingredient>();
    [ForeignKey("recepie_id")]
    public virtual recepie? recepie { get; set; }
}
