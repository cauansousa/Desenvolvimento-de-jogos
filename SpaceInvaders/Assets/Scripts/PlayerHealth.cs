using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3; // Número de vidas do jogador

    public void TakeDamage()
    {
        lives--; // Diminui uma vida
        Debug.Log("Vidas restantes: " + lives);

        // Verifica se o jogador perdeu todas as vidas
        if (lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");
        // adicionar a lógica de game over
    }
}