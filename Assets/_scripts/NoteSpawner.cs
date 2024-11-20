using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public static NoteSpawner Instance; // Singleton

    [SerializeField] GameObject note;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNoteAtTime(int lane, float time)
    {
        // ������: ����� ������ ��
        GameObject notePrefab = note; // ��� ����� �������
        Vector3 spawnPosition = new Vector3(lane * 2, 0, time * 5); // ������: ����� ��� ���� ������
        GameObject curentnout = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
        Note curentnoutnout = curentnout.GetComponent<Note>();
        curentnoutnout.targetTime = time * 5;
        curentnoutnout.lane = lane * 2;
    }

}
