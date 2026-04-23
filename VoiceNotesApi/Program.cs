using Microsoft.EntityFrameworkCore;
using VoiceNotesApi.Application.Interfaces;
using VoiceNotesApi.Application.Services;
using VoiceNotesApi.Infrastructure.Data;
using VoiceNotesApi.Infrastructure.Repositories;
using VoiceNotesApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=notes.db"));

// DI
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<ITranscriptionService, TranscriptionService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowFrontend");
  app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

// GET Notes
app.MapGet("/api/notes", async (INoteService service) =>
{
    return await service.GetAllAsync();
});

// POST Note
app.MapPost("/api/notes", async (INoteService service, Note note) =>
{
    try
    {
        var result = await service.AddAsync(note);
        return Results.Created($"/api/notes/{result.Id}", result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/api/transcribe", async (ITranscriptionService service, IFormFile file) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded");

    var result = await service.TranscribeAsync(file);

    return Results.Ok(new { text = result });

}).DisableAntiforgery();

app.Run();