using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

public class MidiParser : MonoBehaviour
{
    public string midiFilePath = "Assets/MidiFiles/song.mid"; // ���� ����� MIDI
    private List<float[]> noteData = new List<float[]>(); // ����� ������ �������

    [SerializeField]
    private int numberOfLanes = 4; // ���� ������� ����� ����� ��-Inspector

    void Start()
    {
        ParseMidiFile();
        ScheduleNotes(); // ����� �� ������ ������
    }

    void ParseMidiFile()
    {
        // ���� �� ���� �-MIDI
        var midiFile = MidiFile.Read(midiFilePath);

        // ���� �� ����� ������ (Notes)
        var notes = midiFile.GetNotes();

        foreach (var note in notes)
        {
            // ���� ��� MIDI ���� ����� (�����)
            float timeInSeconds = ConvertMidiTimeToSeconds(note.Time, midiFile);

            // ���� �� ���� ����� �����
            int lane = MapNoteToLane(note.NoteName);

            // ����� �� ����� �����
            noteData.Add(new float[] { lane, timeInSeconds });
        }
    }

    float ConvertMidiTimeToSeconds(long midiTime, MidiFile midiFile)
    {
        // ���� BPM �����
        var tempoMap = midiFile.GetTempoMap();
        var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(midiTime, tempoMap);
        return (float)metricTimeSpan.TotalSeconds;
    }

    int MapNoteToLane(NoteName noteName)
    {
        // ����� ������ ������� ����� ����
        switch (noteName)
        {
            case NoteName.C: return (numberOfLanes > 0) ? 0 : -1;
            case NoteName.D: return (numberOfLanes > 1) ? 1 : -1;
            case NoteName.E: return (numberOfLanes > 2) ? 2 : -1;
            case NoteName.F: return (numberOfLanes > 3) ? 3 : -1;
            case NoteName.G: return (numberOfLanes > 4) ? 4 : -1;
            case NoteName.A: return (numberOfLanes > 5) ? 5 : -1;
            case NoteName.B: return (numberOfLanes > 6) ? 6 : -1;
            default: return -1; // ����� ��� �������
        }
    }

    void ScheduleNotes()
    {
        foreach (var note in noteData)
        {
            int lane = (int)note[0];
            float time = note[1];

            // ����� ������ ��� ����
            if (lane == -1)
            {
                Debug.Log($"Note ignored");
                continue;
            }

            // ����� ������ ����� ������
            NoteSpawner.Instance.SpawnNoteAtTime(lane, time);
        }
    }
}
