using UnityEngine;

public class Note : MonoBehaviour
{
    public float targetTime;
    public float lane;
    private bool hasBeenHit = false;

    public void Initialize(float time)
    {
        targetTime = time;
    }

    void Update()
    {
        float timeToTarget = targetTime - GameManager.Instance.GetSongTime();
        transform.position += -Vector3.forward * Time.deltaTime;

        if (!hasBeenHit && timeToTarget < 0)
        {
            // �����
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        hasBeenHit = true;
        Destroy(gameObject);
        // ���� ����� ���
    }
}
