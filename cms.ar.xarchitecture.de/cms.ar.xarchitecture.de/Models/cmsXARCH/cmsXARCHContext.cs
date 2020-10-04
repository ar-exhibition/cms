using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cms.ar.xarchitecture.de.cmsXARCH
{
    public partial class cmsXARCHContext : DbContext
    {
        public cmsXARCHContext()
        {
        }

        public cmsXARCHContext(DbContextOptions<cmsXARCHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anchor> Anchor { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Creator> Creator { get; set; }
        public virtual DbSet<Scene> Scene { get; set; }
        public virtual DbSet<SceneAsset> SceneAsset { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }
        public virtual DbSet<Term> Term { get; set; }
        public virtual DbSet<Thumbnail> Thumbnail { get; set; }

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
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Anchor_SceneAsset");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.CourseId)
                    .HasName("CourseID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Programme)
                    .HasName("fk_Course_Studies1_idx");

                entity.HasIndex(e => e.Term)
                    .HasName("fk_Course_Term1_idx");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Course1)
                    .HasColumnName("Course")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Programme)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Term)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProgrammeNavigation)
                    .WithMany(p => p.Course)
                    .HasPrincipalKey(p => p.Programme)
                    .HasForeignKey(d => d.Programme)
                    .HasConstraintName("fk_Course_Studies");

                entity.HasOne(d => d.TermNavigation)
                    .WithMany(p => p.Course)
                    .HasPrincipalKey(p => p.Term1)
                    .HasForeignKey(d => d.Term)
                    .HasConstraintName("fk_Course_Term");
            });

            modelBuilder.Entity<Creator>(entity =>
            {
                entity.HasIndex(e => e.CreatorId)
                    .HasName("CreatorID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Programme)
                    .HasName("fk_Creator_Studies1_idx");

                entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

                entity.Property(e => e.Creator1)
                    .HasColumnName("Creator")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Programme)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProgrammeNavigation)
                    .WithMany(p => p.Creator)
                    .HasPrincipalKey(p => p.Programme)
                    .HasForeignKey(d => d.Programme)
                    .HasConstraintName("fk_Creator_Studies");
            });

            modelBuilder.Entity<Scene>(entity =>
            {
                entity.HasIndex(e => e.SceneId)
                    .HasName("SceneID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.FileUuid)
                    .HasColumnName("FileUUID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MarkerUuid)
                    .HasColumnName("MarkerUUID")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasComment("UUID");

                entity.Property(e => e.SceneName)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SceneAsset>(entity =>
            {
                entity.HasKey(e => e.AssetId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AssetId)
                    .HasName("AssetID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Course)
                    .HasName("fk_SceneAsset_Course1_idx");

                entity.HasIndex(e => e.Creator)
                    .HasName("fk_SceneAsset_Creator1_idx");

                entity.HasIndex(e => e.ThumbnailUuid)
                    .HasName("ThumbnailUUID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.AssetName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalLink)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FileUuid)
                    .HasColumnName("FileUUID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailUuid)
                    .HasColumnName("ThumbnailUUID")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.CourseNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasForeignKey(d => d.Course)
                    .HasConstraintName("fk_SceneAsset_Course");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.SceneAsset)
                    .HasForeignKey(d => d.Creator)
                    .HasConstraintName("fk_SceneAsset_Creator");

                entity.HasOne(d => d.ThumbnailUu)
                    .WithOne(p => p.SceneAsset)
                    .HasPrincipalKey<Thumbnail>(p => p.ThumbnailUuid)
                    .HasForeignKey<SceneAsset>(d => d.ThumbnailUuid)
                    .HasConstraintName("fk_SceneAsset_Thumbnail1");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.ProgrammeId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Programme)
                    .HasName("Studies_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProgrammeId)
                    .HasName("ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ProgrammeId)
                    .HasColumnName("ProgrammeID")
                    .HasColumnType("int unsigned");

                entity.Property(e => e.Programme)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.HasIndex(e => e.Term1)
                    .HasName("Termn_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.TermId).HasColumnName("TermID");

                entity.Property(e => e.Term1)
                    .IsRequired()
                    .HasColumnName("Term")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Thumbnail>(entity =>
            {
                entity.HasIndex(e => e.ThumbnailUuid)
                    .HasName("ThumbnailUUID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ThumbnailId).HasColumnName("ThumbnailID");

                entity.Property(e => e.ThumbnailUuid)
                    .IsRequired()
                    .HasColumnName("ThumbnailUUID")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
