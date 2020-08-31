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
                entity.HasKey(e => new { e.AnchorId, e.SceneId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("fk_Anchor_SceneAsset1_idx");

                entity.HasIndex(e => e.SceneId)
                    .HasName("fk_Anchor_Scene1_idx");

                entity.Property(e => e.AnchorId).HasColumnName("AnchorID");

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.Rotate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Scale)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Translate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Anchor)
                    .HasPrincipalKey(p => p.AssetId)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("fk_Anchor_SceneAsset1");

                entity.HasOne(d => d.Scene)
                    .WithMany(p => p.Anchor)
                    .HasForeignKey(d => d.SceneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Anchor_Scene1");
            });

            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.Property(e => e.AssetTypeId).HasColumnName("AssetTypeID");

                entity.Property(e => e.Designator)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.CourseId)
                    .HasName("CourseID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Course1)
                    .HasColumnName("Course")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Programme)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Creator>(entity =>
            {
                entity.HasIndex(e => e.CreatorId)
                    .HasName("CreatorID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Studies)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Scene>(entity =>
            {
                entity.HasIndex(e => e.SceneId)
                    .HasName("SceneID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SceneAsset>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.Creator, e.Course })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("AssetID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.AssetType)
                    .HasName("fk_SceneAsset_AssetType1_idx");

                entity.HasIndex(e => e.Course)
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
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetTypeNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasForeignKey(d => d.AssetType)
                    .HasConstraintName("fk_SceneAsset_AssetType1");

                entity.HasOne(d => d.CourseNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasForeignKey(d => d.Course)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SceneAsset_Course1");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SceneAsset_Creator1");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int unsigned");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
