using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Entities;

public partial class user
{
    [Key]
    public int id { get; set; }
        
    public string username { get; set; } = null!;

    public string password { get; set; } = null!;

    [InverseProperty("creator")]
    public virtual ICollection<recepie> recepies { get; set; } = new List<recepie>();
}
