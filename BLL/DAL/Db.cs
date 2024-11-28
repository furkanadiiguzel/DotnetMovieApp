using System;
using System.Collections.Generic;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;
namespace BLL.DAL;

public partial class Db : DbContext
{
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }
    public DbSet<Director> director { get; set; }

    public  DbSet<genre> genre { get; set; }

    public  DbSet<movie> movie { get; set; }

    public  DbSet<moviegenre> moviegenre { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.id).HasName("director_pkey");

            entity.Property(e => e.isretired).HasDefaultValue(false);
        });

        modelBuilder.Entity<genre>(entity =>
        {
            entity.HasKey(e => e.id).HasName("genre_pkey");
        });

        modelBuilder.Entity<movie>(entity =>
        {
            entity.HasKey(e => e.id).HasName("movie_pkey");

            entity.HasOne(d => d.director).WithMany(p => p.movie)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("movie_directorid_fkey");
        });

        modelBuilder.Entity<moviegenre>(entity =>
        {
            entity.HasKey(e => e.id).HasName("moviegenre_pkey");

            entity.HasOne(d => d.genre).WithMany(p => p.moviegenre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("moviegenre_genreid_fkey");

            entity.HasOne(d => d.movie).WithMany(p => p.moviegenre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("moviegenre_movieid_fkey");
        });

      

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}