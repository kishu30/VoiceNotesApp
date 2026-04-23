using VoiceNotesApi.Domain.Entities;

namespace VoiceNotesApi.Infrastructure.Repositories;

public interface INoteRepository
{
    Task<List<Note>> GetAllAsync();
    Task<Note> AddAsync(Note note);
}