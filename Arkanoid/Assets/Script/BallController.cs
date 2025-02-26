using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 5f;
    private Rigidbody2D rb;
    private bool isLaunched = false;
    private Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (!isLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        isLaunched = true;
        rb.isKinematic = false;
        
        // Direção inicial mais controlada (evita ângulos extremos)
        float angle = Random.Range(60f, 120f); // Entre 60° e 120°
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
        rb.linearVelocity = direction.normalized * initialSpeed;
    }

    void FixedUpdate()
    {
        if (isLaunched)
        {
            // Mantém velocidade constante sem reforçar a direção
            rb.linearVelocity = rb.linearVelocity.normalized * initialSpeed;
            lastVelocity = rb.linearVelocity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallDown"))
        {
            if (SceneLoader.Instance != null)
                 SceneLoader.Instance.RestartGame();
            else
                Debug.LogError("SceneLoader não encontrado!");
        }
        // Cálculo de reflexão mais preciso
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectedDir = Vector2.Reflect(lastVelocity.normalized, normal);
        
        // Adiciona variação aleatória na reflexão
        float randomVariation = Random.Range(-10f, 10f);
        reflectedDir = Quaternion.Euler(0, 0, randomVariation) * reflectedDir;
        
        rb.linearVelocity = reflectedDir.normalized * initialSpeed;
        lastVelocity = rb.linearVelocity;

        // Evita loops verticais/horizontais
        PreventAxisLock();

        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject); // Destrói o bloco
            LevelManager.Instance.BrickDestroyed(); // Atualiza o contador de blocos
        }
    }

    void PreventAxisLock()
    {
        Vector2 velocity = rb.linearVelocity;
        
        // Se o movimento estiver muito alinhado aos eixos
        if (Mathf.Abs(velocity.x) < 0.1f || Mathf.Abs(velocity.y) < 0.1f)
        {
            // Adiciona um desvio aleatório
            float randomX = Mathf.Clamp(velocity.x + Random.Range(-0.5f, 0.5f), -1f, 1f);
            float randomY = Mathf.Clamp(velocity.y + Random.Range(-0.5f, 0.5f), -1f, 1f);
            rb.linearVelocity = new Vector2(randomX, randomY).normalized * initialSpeed;
        }
    }
}