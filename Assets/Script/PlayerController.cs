using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 15f;

    [Header("Health & UI")]
    public int maxHealth = 5;
    private int currentHealth;
    public float invincibilityDuration = 0.5f;
    private float invincibilityTimer;
    public TextMeshProUGUI healthText;
    public Image gameOverPanel;

    [Header("Shooting")]
    public GameObject ballPrefab;
    public Transform firePoint;
    public float shootForce = 15f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        DontDestroyOnLoad(this);
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        Move();
        Jump();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincibilityTimer > 0) return;

        currentHealth -= damage;
        invincibilityTimer = invincibilityDuration;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;
        }
    }

    void Die()
    {
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resetPosition()
    {
        transform.position = new Vector3(0, -3, 0);
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();

        Vector2 direction = (mousePos - firePoint.position).normalized;
        ballRb.AddForce(direction * shootForce, ForceMode2D.Impulse);

        Destroy(ball, 1f);
    }
}