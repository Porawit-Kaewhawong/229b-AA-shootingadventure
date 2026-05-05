using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float rayDistance = 2f;
    public float heightOffset = 0.5f;

    private int currentPoint = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (points == null || points.Length == 0) return;

        Vector2 target = points[currentPoint].position;

        float nextX = Mathf.MoveTowards(rb.position.x, target.x, speed * Time.fixedDeltaTime);
        Vector2 nextPos = new Vector2(nextX, rb.position.y);

        RaycastHit2D hit = Physics2D.Raycast(nextPos, Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            nextPos.y = hit.point.y + heightOffset;
        }

        rb.MovePosition(nextPos);

        if (Mathf.Abs(rb.position.x - target.x) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
            Flip(target.x);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Flip(float targetX)
    {
        Vector3 scale = transform.localScale;
        if (targetX > rb.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}