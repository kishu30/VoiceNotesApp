using Microsoft.EntityFrameworkCore;
using VoiceNotesApi.Domain.Entities;
using VoiceNotesApi.Infrastructure.Data;

namespace VoiceNotesApi.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Note>> GetAllAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note> AddAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }
}