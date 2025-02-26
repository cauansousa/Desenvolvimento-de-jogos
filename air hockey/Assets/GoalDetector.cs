using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    [SerializeField] private bool isPlayerGoal; // Marque no Inspector: true para gol do jogador, false para gol da IA

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.Score(isPlayerGoal);
            other.GetComponent<BallController>().ResetPosition();
        }
    }
}                                           