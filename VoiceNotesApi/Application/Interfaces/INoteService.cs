using VoiceNotesApi.Domain.Entities;

namespace VoiceNotesApi.Application.Interfaces;

public interface INoteService
{
    Task<List<Note>> GetAllAsync();
    Task<Note> AddAsync(Note note);
}