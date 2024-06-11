using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class TheGallowContext : DbContext
{
    public TheGallowContext()
    {
    }

    public TheGallowContext(DbContextOptions<TheGallowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-V7DEGCSO\\SQLEXPRESS;Initial Catalog=TheGallow;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Word)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Destroyer).WithMany(p => p.GameDestroyers)
                .HasForeignKey(d => d.Destroyerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Games_Players1");

            entity.HasOne(d => d.Maker).WithMany(p => p.GameMakers)
                .HasForeignKey(d => d.MakerId)
                .HasConstraintName("FK_Games_Players");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
