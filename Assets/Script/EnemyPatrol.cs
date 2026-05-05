using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;

    [Header("Ground Check")]
    public LayerMask groundLayer;   
    public float rayDistance = 2f;  
    public float heightOffset = 0.5f; 

    private int currentPoint = 0;

    void Update()
    {
        if (points.Length == 0) return;

        
        Vector3 target = points[currentPoint].position;
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(target.x, transform.position.y),
            speed * Time.deltaTime
        );

      
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
           
            float newY = hit.point.y + heightOffset;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

      
        if (Mathf.Abs(transform.position.x - target.x) < 0.05f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}