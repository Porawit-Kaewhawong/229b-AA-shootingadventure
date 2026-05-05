using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        Destroy(target);
        Destroy(gameObject);
    }
}
