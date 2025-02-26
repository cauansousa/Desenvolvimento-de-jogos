using UnityEngine;

public class AIRacket : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform ball;
    private Vector2 movementBoundsMin;
    private Vector2 movementBoundsMax;

    [Header("Configurações")]
    [SerializeField] private float moveSpeed = 15f; 
    [SerializeField] private float reactionDistance = 8f; // Distância para começar a reagir
    [SerializeField] private float errorMargin = 0.5f; // Imperfeição humana

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        ball = GameObject.FindGameObjectWithTag("Ball").transform;

        // Configura movimento apenas na parte superior
        GameObject wall = GameObject.FindWithTag("Field");
        if (wall != null)
        {
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            float midY = wallRenderer.bounds.center.y;
            
            movementBoundsMin = new Vector2(
                wallRenderer.bounds.min.x + 1f, 
                midY // Começa do meio do campo para cima
            );
            
            movementBoundsMax = new Vector2(
                wallRenderer.bounds.max.x - 1f, 
                wallRenderer.bounds.max.y - 1f
            );
        }
    }

    void FixedUpdate()
    {
        if (ball == null) return;

        // Só reage se a bola estiver na metade superior
        if (ball.position.y < movementBoundsMin.y) return;

        // Calcula posição alvo com erro controlado
        Vector2 targetPosition = CalculateTargetWithError();
        MoveTowards(targetPosition);
    }

    private Vector2 CalculateTargetWithError()
    {
        // Posição prevista + margem de erro
        Vector2 predictedPos = (Vector2)ball.position + ball.GetComponent<Rigidbody2D>().linearVelocity * 0.5f;
        predictedPos += new Vector2(
            Random.Range(-errorMargin, errorMargin), 
            Random.Range(-errorMargin, errorMargin)
        );

        // Mantém dentro dos limites
        predictedPos.x = Mathf.Clamp(predictedPos.x, movementBoundsMin.x, movementBoundsMax.x);
        predictedPos.y = Mathf.Clamp(predictedPos.y, movementBoundsMin.y, movementBoundsMax.y);

        return predictedPos;
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        // Move-se suavemente e evita trepidação
        if (Vector2.Distance(rb.position, targetPosition) > 0.2f)
        {
            rb.MovePosition(
                Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime)
            );
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Adiciona um impulso aleatório à bola para variar a trajetória
        if (coll.gameObject.CompareTag("Ball"))
        {
            Vector2 randomForce = new Vector2(
                Random.Range(-3f, 3f), 
                Random.Range(-2f, 2f)
            );
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(randomForce, ForceMode2D.Impulse);
        }
    }
    public void ResetPosition()
    {
    // Configure a posição inicial da raquete
    GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    transform.position = new Vector2(3, 0);
    
    // Exemplo para posição fixa:
    // transform.position = new Vector2(7.5f, 0); 
    }
}