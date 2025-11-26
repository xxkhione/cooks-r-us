using System;
using System.Collections.Generic;
using CooksRUs.Entities;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Database;

public partial class CooksRUsDbContext : DbContext
{
    public CooksRUsDbContext()
    {
    }

    public CooksRUsDbContext(DbContextOptions<CooksRUsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<faved_recepie> faved_recepies { get; set; }

    public virtual DbSet<ingredient> ingredients { get; set; }

    public virtual DbSet<ingredient_list> ingredient_lists { get; set; }

    public virtual DbSet<recepie> recepies { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HJOHNSON;Initial Catalog=CooksRUs;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<faved_recepie>(entity =>
        {
            entity.HasOne(d => d.recepie).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_faved_recepies_recepies1");

            entity.HasOne(d => d.user).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_faved_recepies_users");
        });

        modelBuilder.Entity<ingredient>(entity =>
        {
            entity.Property(e => e.amount).IsFixedLength();

            entity.HasOne(d => d.list).WithMany(p => p.ingredients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ingredients_ingredient_lists");
        });

        modelBuilder.Entity<recepie>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_recepies_1");

            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.creator).WithMany(p => p.recepies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recepies_users1");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
