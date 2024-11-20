using UnityEngine;

public class Note : MonoBehaviour
{
    public float targetTime;
    private bool hasBeenHit = false;

    public void Initialize(float time)
    {
        targetTime = time;
    }

    void Update()
    {
        float timeToTarget = targetTime - GameManager.Instance.GetSongTime();
        transform.position += Vector3.down * Time.deltaTime;

        if (!hasBeenHit && timeToTarget < 0)
        {
            // פספוס
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        hasBeenHit = true;
        Destroy(gameObject);
        // הוסף ניקוד כאן
    }
}
