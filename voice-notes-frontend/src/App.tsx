import { useState, useRef, useEffect } from "react";
import "./app.css";

function App() {
  const [recording, setRecording] = useState(false);
  const [loading, setLoading] = useState(false);
  const [text, setText] = useState("");
  const [notes, setNotes] = useState<any[]>([]);

  const mediaRecorderRef = useRef<MediaRecorder | null>(null);
  const chunksRef = useRef<Blob[]>([]);

  const startRecording = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
      const mediaRecorder = new MediaRecorder(stream);

      mediaRecorderRef.current = mediaRecorder;

      mediaRecorder.ondataavailable = (e) => {
        chunksRef.current.push(e.data);
      };

      mediaRecorder.start();
      setRecording(true);
    } catch {
      alert("Microphone permission denied");
    }
  };

  const stopRecording = () => {
    mediaRecorderRef.current?.stop();
    setRecording(false);

    mediaRecorderRef.current!.onstop = async () => {
      const blob = new Blob(chunksRef.current, { type: "audio/webm" });
      chunksRef.current = [];

      const formData = new FormData();
      formData.append("file", blob, "recording.webm");

      try {
        setLoading(true);

        const res = await fetch("http://localhost:5069/api/transcribe", {
          method: "POST",
          body: formData,
        });

        const data = await res.json();
        setText(data.text);
      } catch {
        alert("Transcription failed");
      } finally {
        setLoading(false);
      }
    };
  };

  const saveNote = async () => {
    if (!text.trim()) return;

    await fetch("http://localhost:5069/api/notes", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ content: text }),
    });

    setText("");
    fetchNotes();
  };

  const fetchNotes = async () => {
    const res = await fetch("http://localhost:5069/api/notes");
    const data = await res.json();
    setNotes(data);
  };

  useEffect(() => {
    fetchNotes();
  }, []);

  const formatDate = (date: string) => {
    return new Date(date).toLocaleString("en-IN", {
      day: "2-digit",
      month: "short",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  return (
    <div className="page">
      <div className="container">
        <h1 className="title">Voice Notes</h1>

        <div className="card">
          <div className="recorder-row">
            <button
              onClick={recording ? stopRecording : startRecording}
              disabled={loading}
              className={`record-button ${
                recording ? "recording" : "not-recording"
              }`}
            >
              {recording ? "Stop Recording" : "Start Recording"}
            </button>

            {recording && <span className="status">Recording...</span>}
            {loading && <span className="status">Processing...</span>}
          </div>

          <textarea
            className="textarea"
            value={text}
            onChange={(e) => setText(e.target.value)}
            placeholder="Your transcribed text will appear here..."
          />

          <button className="save-button" onClick={saveNote}>
            Save Note
          </button>
        </div>

        <div className="notes-section">
          <h2 className="section-title">Saved Notes</h2>

          <div className="grid">
            {notes.map((note) => (
              <div key={note.id} className="note-card">
                <p className="note-text">{note.content}</p>
                <span className="timestamp">
                  {formatDate(note.createdAt)}
                </span>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;