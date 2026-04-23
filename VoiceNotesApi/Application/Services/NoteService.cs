using VoiceNotesApi.Application.Interfaces;
using VoiceNotesApi.Domain.Entities;
using VoiceNotesApi.Infrastructure.Repositories;

namespace VoiceNotesApi.Application.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _repo;

    public NoteService(INoteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Note>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Note> AddAsync(Note note)
    {
        if (string.IsNullOrWhiteSpace(note.Content))
            throw new ArgumentException("Content cannot be empty");

        return await _repo.AddAsync(note);
    }
}