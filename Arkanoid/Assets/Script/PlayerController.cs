using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private float minX = -3.5f; // Limite esquerdo
    private float maxX = 3.5f;  // Limite direito

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 newPosition = rb.position + Vector2.right * moveHorizontal * speed * Time.fixedDeltaTime;
        
        // Aplica os limites de movimento
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        
        rb.MovePosition(newPosition);
    }
}