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

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0789569AD7");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F6767023BC").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3214EC07EFDB8D93");

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C9911435F289BD0");

            entity.Property(e => e.ArticleNumber).HasMaxLength(256);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasMany(d => d.Categories).WithMany(p => p.ArticleNumbers)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade) //Jag hatar mitt liv allt jag behövde göra var att ändra denna till Cascade ajsdhakjshdads
                        .HasConstraintName("FK__ProductCa__Categ__797309D9"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ProductCa__Artic__787EE5A0"),
                    j =>
                    {
                        j.HasKey("ArticleNumber", "CategoryId").HasName("PK__ProductC__9D0982E351781097");
                        j.ToTable("ProductCategories");
                        j.IndexerProperty<string>("ArticleNumber").HasMaxLength(256);
                    });
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__7516F70C32A13750");

            entity.Property(e => e.ImageId).ValueGeneratedNever();
            entity.Property(e => e.ArticleNumber).HasMaxLength(256);

            entity.HasOne(d => d.ArticleNumberNavigation).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ArticleNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Artic__7D439ABD");

            entity.HasOne(d => d.Image).WithOne(p => p.ProductImage)
                .HasForeignKey<ProductImage>(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Image__7C4F7684");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__ProductR__74BC79CEF113E4B5");

            entity.Property(e => e.ReviewId).ValueGeneratedNever();
            entity.Property(e => e.ArticleNumber).HasMaxLength(256);

            entity.HasOne(d => d.ArticleNumberNavigation).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ArticleNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductRe__Artic__01142BA1");

            entity.HasOne(d => d.Review).WithOne(p => p.ProductReview)
                .HasForeignKey<ProductReview>(d => d.ReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductRe__Revie__00200768");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC0737F58565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
