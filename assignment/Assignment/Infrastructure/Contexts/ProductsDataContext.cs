using System;
using System.Collections.Generic;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class ProductsDataContext : DbContext
{
    public ProductsDataContext()
    {
    }

    public ProductsDataContext(DbContextOptions<ProductsDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07A0429660");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F654F859FC").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3214EC07AE5CB1BD");

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

            entity.HasMany(d => d.ArticleNumbers).WithMany(p => p.Images)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductImage",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductIm__Artic__51300E55"),
                    l => l.HasOne<Image>().WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductIm__Image__503BEA1C"),
                    j =>
                    {
                        j.HasKey("ImageId", "ArticleNumber").HasName("PK__ProductI__56DF66189C976FBD");
                        j.ToTable("ProductImages");
                        j.IndexerProperty<string>("ArticleNumber").HasMaxLength(256);
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C99114398E5DE18");

            entity.Property(e => e.ArticleNumber).HasMaxLength(256);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasMany(d => d.Categories).WithMany(p => p.ArticleNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductCa__Categ__4D5F7D71"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductCa__Artic__4C6B5938"),
                    j =>
                    {
                        j.HasKey("ArticleNumber", "CategoryId").HasName("PK__ProductC__9D0982E303406A3F");
                        j.ToTable("ProductCategories");
                        j.IndexerProperty<string>("ArticleNumber").HasMaxLength(256);
                    });
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC0747219ABE");

            entity.HasMany(d => d.ArticleNumbers).WithMany(p => p.Reviews)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductReview",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductRe__Artic__55009F39"),
                    l => l.HasOne<Review>().WithMany()
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductRe__Revie__540C7B00"),
                    j =>
                    {
                        j.HasKey("ReviewId", "ArticleNumber").HasName("PK__ProductR__5775E8DAE43F1BE9");
                        j.ToTable("ProductReviews");
                        j.IndexerProperty<string>("ArticleNumber").HasMaxLength(256);
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
