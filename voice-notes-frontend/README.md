# Voice Notes PWA

## 🧠 Overview

Voice Notes is a mobile-first Progressive Web App (PWA) that allows users to:

- Record voice notes using the device microphone  
- Convert speech to text via backend transcription  
- Edit transcribed content  
- Save notes  
- View saved notes  

The application focuses on clean architecture, responsiveness, and usability.

---

## 🚀 Features

- Audio recording using MediaRecorder API  
- Speech-to-text transcription (Deepgram API)  
- Editable text before saving  
- Persistent storage using SQLite  
- Notes list with formatted timestamps  
- Mobile-first responsive UI  
- Basic error handling (mic permission, API failure)

---

## ⚙️ Setup Instructions

### 1. Clone Repository

```bash
git clone https://github.com/amanpriyadarshi/VoiceNotesApp.git
cd VoiceNotesApp


2. Backend Setup (.NET 8)
cd VoiceNotesApi
dotnet restore
dotnet build
dotnet run

Backend runs on:

http://localhost:5069
3. Frontend Setup (React + Vite)
cd voice-notes-frontend
npm install
npm run dev

Frontend runs on:

http://localhost:5173
4. Database
SQLite is used for persistence
Database file (notes.db) is auto-created
🏗️ Architecture Overview
Frontend
React + Vite
MediaRecorder API for audio capture
Handles UI state and API communication
Backend

Structured in layered approach:

Controller → Service → Data Layer
Controllers: API endpoints (/transcribe, /notes)
Services: Business logic
Data Layer: EF Core + SQLite
Speech-to-Text Flow
Audio (Frontend)
   ↓
Backend API (/transcribe)
   ↓
Deepgram API
   ↓
Text → Frontend
🧾 Assumptions
Audio format: webm (browser supported)
No authentication required (demo scope)
Uses external STT API (Deepgram free tier)
Data stored locally (SQLite)
Optimized for mobile / Chrome emulation
📚 Libraries & Services
Frontend
React
Vite
MediaRecorder API
Backend
.NET 8 Web API
Entity Framework Core
SQLite
External
Deepgram (Speech-to-Text)
⚠️ Error Handling
Microphone permission denial handled
API failure handled with fallback message
Empty note validation before saving
📱 PWA Support
Mobile-first UI
Works in Chrome mobile emulation
Lightweight and fast
🎯 Design Decisions
Backend-based transcription for better control
SQLite for simplicity and quick setup
Clean separation of concerns
Focus on usability over heavy UI frameworks