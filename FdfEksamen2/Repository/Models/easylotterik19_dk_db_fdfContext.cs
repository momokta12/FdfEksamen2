using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FDFVANLØSEEKSAMEN.Repository.Models
{
    public partial class easylotterik19_dk_db_fdfContext : DbContext
    {
        public easylotterik19_dk_db_fdfContext()
        {
        }

        public easylotterik19_dk_db_fdfContext(DbContextOptions<easylotterik19_dk_db_fdfContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Barn> Barns { get; set; } = null!;
        public virtual DbSet<Betaling> Betalings { get; set; } = null!;
        public virtual DbSet<Bruger> Brugers { get; set; } = null!;
        public virtual DbSet<BørneGruppe> BørneGruppes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=mssql1.unoeuro.com;User ID=easylotterik19_dk;Password=eR3HDGycFhbk9xABm2Eg;Initial Catalog=easylotterik19_dk_db_fdf;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barn>(entity =>
            {
                entity.ToTable("Barn");

                entity.Property(e => e.BarnId).HasColumnName("BarnID");

                entity.Property(e => e.Bgid).HasColumnName("BGID");

                entity.Property(e => e.Bnavn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BNavn");

                entity.Property(e => e.ModtagetLod).HasColumnName("ModtagetLOD");

                entity.Property(e => e.RetuneretLod).HasColumnName("RetuneretLOD");

                entity.HasOne(d => d.Bg)
                    .WithMany(p => p.Barns)
                    .HasForeignKey(d => d.Bgid)
                    .HasConstraintName("FK_Child_ChildGroup");
            });

            modelBuilder.Entity<Betaling>(entity =>
            {
                entity.HasKey(e => e.BetalingsId)
                    .HasName("PK_Payment");

                entity.ToTable("Betaling");

                entity.Property(e => e.BetalingsId).HasColumnName("BetalingsID");

                entity.Property(e => e.AntalLod).HasColumnName("AntalLOD");

                entity.Property(e => e.BarnId).HasColumnName("BarnID");

                entity.Property(e => e.BetalingsDato).HasColumnType("datetime");

                entity.HasOne(d => d.Barn)
                    .WithMany(p => p.Betalings)
                    .HasForeignKey(d => d.BarnId)
                    .HasConstraintName("FK_Payment_Child");
            });

            modelBuilder.Entity<Bruger>(entity =>
            {
                entity.ToTable("Bruger");

                entity.Property(e => e.BrugerId).HasColumnName("BrugerID");

                entity.Property(e => e.Brugernavn)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kodeord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rolle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BørneGruppe>(entity =>
            {
                entity.HasKey(e => e.Bgid)
                    .HasName("PK_ChildGroup");

                entity.ToTable("BørneGruppe");

                entity.Property(e => e.Bgid).HasColumnName("BGID");

                entity.Property(e => e.Bgnavn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BGNavn");

                entity.Property(e => e.LederId).HasColumnName("LederID");

                entity.Property(e => e.RetuneretLod).HasColumnName("RetuneretLOD");

                entity.HasOne(d => d.Leder)
                    .WithMany(p => p.BørneGruppes)
                    .HasForeignKey(d => d.LederId)
                    .HasConstraintName("FK_ChildGroup_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
