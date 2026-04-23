using Microsoft.AspNetCore.Http;

namespace VoiceNotesApi.Application.Interfaces;

public interface ITranscriptionService
{
    Task<string> TranscribeAsync(IFormFile file);
}