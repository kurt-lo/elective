using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace elective.Entities;

public partial class QueueDbContext : DbContext
{
    public QueueDbContext()
    {
    }

    public QueueDbContext(DbContextOptions<QueueDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Queue> Queues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Queue>(entity =>
        {
            entity.HasKey(e => e.Idqueue).HasName("PRIMARY");

            entity.ToTable("queue");

            entity.Property(e => e.Queue_number);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
