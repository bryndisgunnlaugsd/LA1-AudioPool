using AudioPool.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repositories.Contexts;

public class AudioPoolDbContext : DbContext
{
    public AudioPoolDbContext(DbContextOptions<AudioPoolDbContext> options) : base(options) { }

    public DbSet<Album> Albums { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Song> Songs { get; set; } = null!;
}