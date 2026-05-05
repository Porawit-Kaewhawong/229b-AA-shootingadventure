using UnityEngine;

public class HoopScore : MonoBehaviour
{
    public ScoreManager scoreManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            
            if (rb.linearVelocity.y < 0)
            {
                scoreManager.AddScore();
            }
        }
    }
}