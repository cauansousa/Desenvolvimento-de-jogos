using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Chamado pelo botão "Jogar"
    public void Jogar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    // Se quiser um botão "Sair"
    public void Sair()
    {
        Application.Quit();
    }
}
