using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

public class MidiParser : MonoBehaviour
{
    public string midiFilePath = "Assets/MidiFiles/song.mid"; // נתיב לקובץ MIDI
    private List<float[]> noteData = new List<float[]>(); // רשימה לאחסון הנתונים

    [SerializeField]
    private int numberOfLanes = 4; // מספר הנתיבים שניתן לשנות מה-Inspector

    void Start()
    {
        ParseMidiFile();
        ScheduleNotes(); // שיגור כל התווים למערכת
    }

    void ParseMidiFile()
    {
        // טוען את קובץ ה-MIDI
        var midiFile = MidiFile.Read(midiFilePath);

        // מקבל את רשימת התווים (Notes)
        var notes = midiFile.GetNotes();

        foreach (var note in notes)
        {
            // ממיר זמן MIDI לזמן במשחק (שניות)
            float timeInSeconds = ConvertMidiTimeToSeconds(note.Time, midiFile);

            // ממפה את גובה הצליל לנתיב
            int lane = MapNoteToLane(note.NoteName);

            // מוסיף את הנתון למערך
            noteData.Add(new float[] { lane, timeInSeconds });
        }
    }

    float ConvertMidiTimeToSeconds(long midiTime, MidiFile midiFile)
    {
        // מחשב BPM מהשיר
        var tempoMap = midiFile.GetTempoMap();
        var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(midiTime, tempoMap);
        return (float)metricTimeSpan.TotalSeconds;
    }

    int MapNoteToLane(NoteName noteName)
    {
        // מיפוי צלילים לנתיבים באופן ישיר
        switch (noteName)
        {
            case NoteName.C: return (numberOfLanes > 0) ? 0 : -1;
            case NoteName.D: return (numberOfLanes > 1) ? 1 : -1;
            case NoteName.E: return (numberOfLanes > 2) ? 2 : -1;
            case NoteName.F: return (numberOfLanes > 3) ? 3 : -1;
            case NoteName.G: return (numberOfLanes > 4) ? 4 : -1;
            case NoteName.A: return (numberOfLanes > 5) ? 5 : -1;
            case NoteName.B: return (numberOfLanes > 6) ? 6 : -1;
            default: return -1; // תווים שלא מתאימים
        }
    }

    void ScheduleNotes()
    {
        foreach (var note in noteData)
        {
            int lane = (int)note[0];
            float time = note[1];

            // התעלם מתווים ללא נתיב
            if (lane == -1)
            {
                Debug.Log($"Note ignored");
                continue;
            }

            // שיגור למערכת יצירת התווים
            NoteSpawner.Instance.SpawnNoteAtTime(lane, time);
        }
    }
}
