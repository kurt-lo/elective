using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace elective.Entities;

public partial class ElectiveDbContext : DbContext
{
    public ElectiveDbContext()
    {
    }

    public ElectiveDbContext(DbContextOptions<ElectiveDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Enrollmentdate).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(45);
            entity.Property(e => e.Lastname).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Username).HasMaxLength(45);
            entity.Property(e => e.Queue_number);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
