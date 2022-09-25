using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EfCore.Models;

#nullable disable

namespace EfCore.Data
{
    public partial class AUMSContext : DbContext
    {
        public AUMSContext()
        {
        }

        public AUMSContext(DbContextOptions<AUMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IpInfo> IpInfos { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(Local);Database=AUMS;User Id=MT;Password=q1w2e3r4t5Y^U&I*O(P);Connection Timeout=3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<IpInfo>(entity =>
            {
                entity.HasIndex(e => e.IpAddress, "IX_IpInfo_1");

                entity.HasIndex(e => e.IpAddress, "UQ_IpInfo_1")
                    .IsUnique();

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PermissionYn).HasColumnName("PermissionYN");

                entity.Property(e => e.UseYn).HasColumnName("UseYN");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.IpInfos)
                    .HasForeignKey(d => d.UserInfoId)
                    .HasConstraintName("FK_IpInfos_UserInfoId_UserInfos_UserInfoId");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasIndex(e => new { e.EmpNo, e.CmpCode }, "IX_UserInfo_1");

                entity.HasIndex(e => new { e.EmpNo, e.CmpCode }, "UQ_UserInfo_1")
                    .IsUnique();

                entity.Property(e => e.CmpCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.EmpNo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
