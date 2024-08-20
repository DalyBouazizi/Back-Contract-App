using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Projet_Data.ModelsEF;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DALY-S-PC\\SQL2022;Initial Catalog=sebndb;User ID=sa;Password=0000;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Idcontrat).HasName("PK__Contract__AAF11F01007753E0");

            entity.HasOne(d => d.Employee).WithMany(p => p.Contracts).HasConstraintName("FK__Contract__Employ__3B75D760");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07165D8262");

            entity.HasMany(d => d.Users).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__EmployeUs__UserI__4222D4EF"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__EmployeUs__Emplo__412EB0B6"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "UserId").HasName("PK__EmployeU__ABA8C3D546AD03EE");
                        j.ToTable("EmployeUser");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC076A7DF11D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
