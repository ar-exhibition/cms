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
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Creator> Creator { get; set; }
        public virtual DbSet<LightAnchor> LightAnchor { get; set; }
        public virtual DbSet<LightAsset> LightAsset { get; set; }
        public virtual DbSet<MarkerAnchor> MarkerAnchor { get; set; }
        public virtual DbSet<MarkerAsset> MarkerAsset { get; set; }
        public virtual DbSet<Scene> Scene { get; set; }
        public virtual DbSet<SceneAsset> SceneAsset { get; set; }
        public virtual DbSet<SceneAssetAnchor> SceneAssetAnchor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=luziffer.ddnss.de;port=3306;user=root;password=vdh;database=cmsDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anchor>(entity =>
            {
                entity.HasKey(e => new { e.AnchorId, e.SceneId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.SceneId)
                    .HasName("fk_Anchor_Scene1_idx");

                entity.Property(e => e.AnchorId).HasColumnName("AnchorID");

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.Rotate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Scale)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Translate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Scene)
                    .WithMany(p => p.Anchor)
                    .HasForeignKey(d => d.SceneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Anchor_Scene1");
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

            modelBuilder.Entity<LightAnchor>(entity =>
            {
                entity.HasKey(e => new { e.AnchorId, e.AssetId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("fk_LightAnchor_LightAsset1_idx");

                entity.Property(e => e.AnchorId).HasColumnName("AnchorID");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.LightAnchor)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_LightAnchor_LightAsset1");
            });

            modelBuilder.Entity<LightAsset>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("AssetID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.Color)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MarkerAnchor>(entity =>
            {
                entity.HasKey(e => new { e.AnchorId, e.AssetId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("fk_MarkerAnchor_MarkerAsset1_idx");

                entity.Property(e => e.AnchorId).HasColumnName("AnchorID");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.MarkerAnchor)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MarkerAnchor_MarkerAsset1");
            });

            modelBuilder.Entity<MarkerAsset>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("AssetID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.Link)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
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
                entity.HasKey(e => new { e.AssetId, e.Course, e.Creator })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("AssetID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Course)
                    .HasName("fk_SceneAsset_Course1_idx");

                entity.HasIndex(e => e.Creator)
                    .HasName("fk_SceneAsset_Creator1_idx");

                entity.Property(e => e.AssetId)
                    .HasColumnName("AssetID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Filename)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Filetype)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(45)
                    .IsUnicode(false);

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

            modelBuilder.Entity<SceneAssetAnchor>(entity =>
            {
                entity.HasKey(e => new { e.AnchorId, e.AssetId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("fk_SceneAssetAnchor_SceneAsset1_idx");

                entity.Property(e => e.AnchorId).HasColumnName("AnchorID");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.SceneAssetAnchor)
                    .HasPrincipalKey(p => p.AssetId)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SceneAssetAnchor_SceneAsset1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
