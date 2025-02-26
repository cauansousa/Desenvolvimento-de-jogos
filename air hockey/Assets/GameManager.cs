using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text aiScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text winnerText;

    [Header("Game Objects")]
    [SerializeField] private BallController ball;
    [SerializeField] private UserRacket playerRacket;
    [SerializeField] private AIRacket aiRacket;

    private int playerScore = 0;
    private int aiScore = 0;
    private const int maxScore = 7;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeReferences();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeReferences()
    {
        // Busca automática se não atribuído no Inspector
        if (ball == null)
            ball = FindObjectOfType<BallController>();
        
        if (playerRacket == null)
            playerRacket = FindObjectOfType<UserRacket>();
        
        if (aiRacket == null)
            aiRacket = FindObjectOfType<AIRacket>();

        // Busca por tags
        if (playerScoreText == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("PlayerScore");
            if (obj != null) playerScoreText = obj.GetComponent<TMP_Text>();
        }

        if (aiScoreText == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("AIScore");
            if (obj != null) aiScoreText = obj.GetComponent<TMP_Text>();
        }

        UpdateScoreDisplay();
    }

    public void Score(bool isPlayerGoal)
    {
        if (isPlayerGoal) aiScore++;
        else playerScore++;

        UpdateScoreDisplay();
        CheckGameOver();
        ResetGameElements();
    }

    void UpdateScoreDisplay()
    {
        if (playerScoreText != null)
            playerScoreText.text = playerScore.ToString();
            
        if (aiScoreText != null)
            aiScoreText.text = aiScore.ToString();
    }

    private void CheckGameOver()
    {
        if (playerScore >= maxScore || aiScore >= maxScore)
        {
            // Mostra a mensagem de vitória
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                winnerText.text = (playerScore >= maxScore) ? "JOGADOR VENCEU!" : "IA VENCEU!";
            }
            
            // Reinicia o jogo após 3 segundos
            StartCoroutine(RestartAfterDelay(3f));
        }
    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        // Espera o tempo definido ignorando o Time.timeScale
        yield return new WaitForSecondsRealtime(delay);
        
        // Reinicia o jogo
        RestartGame();
        
        // Esconde o painel de vitória
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void ResetGameElements()
    {
        if (ball != null)
        {
            ball.ResetPosition();
            ball.Invoke("LaunchBall", 2f);
        }
        if (playerRacket != null) playerRacket.ResetPosition();
        if (aiRacket != null) aiRacket.ResetPosition();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        playerScore = 0;
        aiScore = 0;
        UpdateScoreDisplay();
        
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        ResetGameElements();
    }
}