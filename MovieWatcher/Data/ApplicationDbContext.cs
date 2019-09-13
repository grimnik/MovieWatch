using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWatcher.Domain;

namespace MovieWatcher.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<GezienStatus> GezienStatuses { get; set; }
        public DbSet<UserFilmGezienStatus> UserFilmGeziens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFilmGezienStatus>().HasKey(uf => new { uf.FilmId, uf.UserId });
            builder.Entity<Rating>().HasKey(r => new { r.FilmId, r.UserId });
            builder.Entity<GezienStatus>().HasData(
                new GezienStatus() { Id = 1, Naam = "Wil ik zien" },
                new GezienStatus() { Id = 2, Naam = "Gezien" }
                );
            base.OnModelCreating(builder);
        }
    }
}
