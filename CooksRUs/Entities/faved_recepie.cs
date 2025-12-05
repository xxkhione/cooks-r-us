using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Entities;

public partial class faved_recepie
{
    public int user_id { get; set; }

    public int recepie_id { get; set; }

    [ForeignKey("recepie_id")]
    public virtual recepie recepie { get; set; } = null!;

    [ForeignKey("user_id")]
    public virtual user user { get; set; } = null!;
}
