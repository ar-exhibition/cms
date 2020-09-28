using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cms.ar.xarchitecture.de.cmsDatabase
{
    public partial class cmsDatabaseContext : DbContext
    {
        public cmsDatabaseContext()
        {
        }

        public cmsDatabaseContext(DbContextOptions<cmsDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anchor> Anchor { get; set; }
        public virtual DbSet<AssetType> AssetType { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Creator> Creator { get; set; }
        public virtual DbSet<Scene> Scene { get; set; }
        public virtual DbSet<SceneAsset> SceneAsset { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anchor>(entity =>
            {
                entity.HasIndex(e => e.AnchorId)
                    .HasName("AnchorID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.AssetId)
                    .HasName("fk_Anchor_SceneAsset1_idx");

                entity.Property(e => e.AnchorId)
                    .HasColumnName("AnchorID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.Scale).HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Anchor)
                    .HasPrincipalKey(p => p.AssetId)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Anchor_SceneAsset");
            });

            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.Property(e => e.AssetTypeId).HasColumnName("AssetTypeID");

                entity.Property(e => e.Designator)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.Programme })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CourseId)
                    .HasName("CourseID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Programme)
                    .HasName("fk_Course_Studies1_idx");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Programme)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProgrammeNavigation)
                    .WithMany(p => p.Course)
                    .HasPrincipalKey(p => p.Programme)
                    .HasForeignKey(d => d.Programme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Course_Studies");
            });

            modelBuilder.Entity<Creator>(entity =>
            {
                entity.HasKey(e => new { e.CreatorId, e.Programme })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CreatorId)
                    .HasName("CreatorID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Programme)
                    .HasName("fk_Creator_Studies1_idx");

                entity.Property(e => e.CreatorId)
                    .HasColumnName("CreatorID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Programme)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProgrammeNavigation)
                    .WithMany(p => p.Creator)
                    .HasPrincipalKey(p => p.Programme)
                    .HasForeignKey(d => d.Programme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Creator_Studies");
            });

            modelBuilder.Entity<Scene>(entity =>
            {
                entity.HasIndex(e => e.SceneId)
                    .HasName("SceneID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.MarkerFile)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasComment("Link");

                entity.Property(e => e.MarkerName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasComment("UUID");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SceneFile)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasComment("Link");

                entity.Property(e => e.SceneFileName)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SceneAsset>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.Creator, e.CourseName })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("AssetID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.AssetType)
                    .HasName("fk_SceneAsset_AssetType1_idx");

                entity.HasIndex(e => e.CourseName)
                    .HasName("fk_SceneAsset_Course1_idx");

                entity.HasIndex(e => e.Creator)
                    .HasName("fk_SceneAsset_Creator1_idx");

                entity.Property(e => e.AssetId)
                    .HasColumnName("AssetID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Color)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Filename)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Filetype)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Power)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasComment("Also Link to File!");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetTypeNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasForeignKey(d => d.AssetType)
                    .HasConstraintName("fk_SceneAsset_AssetType");

                entity.HasOne(d => d.CourseNameNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasPrincipalKey(p => p.CourseId)
                    .HasForeignKey(d => d.CourseName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SceneAsset_Course");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasPrincipalKey(p => p.CreatorId)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SceneAsset_Creator");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Programme)
                    .HasName("Studies_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int unsigned");

                entity.Property(e => e.Programme)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
