using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    public float horizontalSpeed = 5f; // Velocidade horizontal
    public float direction = 1f;       // Direção (1 = direita, -1 = esquerda)

    void Update()
    {
        // Movimento horizontal
        transform.Translate(Vector2.right * direction * horizontalSpeed * Time.deltaTime);

        // Destrói a nave ao sair da tela
        if ((direction == 1f && transform.position.x > 12f) || 
            (direction == -1f && transform.position.x < -12f))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(float newDirection)
    {
        direction = newDirection;
    }
}