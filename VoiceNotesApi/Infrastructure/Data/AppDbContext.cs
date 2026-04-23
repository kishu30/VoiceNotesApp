using Microsoft.EntityFrameworkCore;
using VoiceNotesApi.Domain.Entities;
namespace VoiceNotesApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Note> Notes => Set<Note>();
}