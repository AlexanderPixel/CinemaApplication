using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Cinema.DAL.Context
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
            : base("name=CinemaContext")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<CinemaHall> CinemaHalls { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmActor> FilmActors { get; set; }
        public virtual DbSet<MovieTheater> MovieTheaters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .HasMany(e => e.FilmActors)
                .WithRequired(e => e.Actor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.FilmActors)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovieTheater>()
                .HasMany(e => e.CinemaHalls)
                .WithRequired(e => e.MovieTheater)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
