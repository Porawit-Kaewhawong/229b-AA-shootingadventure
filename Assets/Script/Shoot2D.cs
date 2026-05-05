using UnityEngine;

public class Shoot2D : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform firePoint;
    public float shootForce = 15f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

       
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

       
        Vector2 direction = (mousePos - firePoint.position).normalized;

        
        rb.AddForce(direction * shootForce, ForceMode2D.Impulse);

        
        Destroy(ball, 1f);
    }
}