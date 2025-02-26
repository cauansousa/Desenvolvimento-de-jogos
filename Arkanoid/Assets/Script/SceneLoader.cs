using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Singleton
    public static SceneLoader Instance;

    void Awake()
    {
        // Configura o Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém este objeto ao carregar outras cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método que verifica a cena atual e carrega a próxima (por ex.: se está em "nivel1", carrega "nivel2")
    public void IrParaProximoNivel()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;

        if (cenaAtual == "Nivel1")
        {
            SceneManager.LoadScene("Nivel2");
        }
        else if (cenaAtual == "Nivel2")
        {
            SceneManager.LoadScene("GameWin");
        }
        else if (cenaAtual == "Menu")
        {
            SceneManager.LoadScene("Nivel1");
        }
        else
        {
            Debug.LogWarning("Não há lógica de transição para a cena: " + cenaAtual);
        }
    }

    // Método para reiniciar o jogo, voltando ao menu
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}