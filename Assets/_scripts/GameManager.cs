using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource musicPlayer;
    public string selectedSongPath;
    public float songStartTime;
    [SerializeField] NoteSpawner NoteSpawner;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        musicPlayer.Play();
        songStartTime = Time.time;
        GenerateNotes();
    }

    public float GetSongTime()
    {
        return Time.time - songStartTime;
    }

    private void GenerateNotes()
    {

    }
}