using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; // Velocidade do projétil

    void Start()
    {
        // Registra o tiro no gerenciador
        EnemyBulletManager.Instance.RegisterBullet(gameObject);
    }

    void Update()
    {

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player atingido");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(); // Reduz uma vida do jogador
            }

            Destroy(gameObject); 
        }
        else if (other.CompareTag("Barreira"))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        EnemyBulletManager.Instance.UnregisterBullet(gameObject);
    }
}