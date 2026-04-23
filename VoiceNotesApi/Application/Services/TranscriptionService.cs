using System.Net.Http.Headers;
using System.Text.Json;
using VoiceNotesApi.Application.Interfaces;

public class TranscriptionService : ITranscriptionService
{
    public async Task<string> TranscribeAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Invalid file");
        var apiKey = ""; // use te api key from the email
        using var stream = file.OpenReadStream();

        var client = new HttpClient();

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.deepgram.com/v1/listen"
        );

        request.Headers.Add("Authorization", $"Token {apiKey}");

        request.Content = new StreamContent(stream);
        request.Content.Headers.ContentType =
            new MediaTypeHeaderValue("audio/webm");

        var response = await client.SendAsync(request);

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(result);

        // Parse Deepgram response
        var json = JsonDocument.Parse(result);

        var text = json
            .RootElement
            .GetProperty("results")
            .GetProperty("channels")[0]
            .GetProperty("alternatives")[0]
            .GetProperty("transcript")
            .GetString();

        return text ?? "No transcription";
    }
}