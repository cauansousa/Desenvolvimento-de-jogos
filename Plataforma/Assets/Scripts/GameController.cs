using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public int totalScore; // nossa pontuacao total
    public static GameController instance; // assim eu posso chamar qualquer variavel nao privada em outras classes
    public TMP_Text scoreText; // nossa pontuacao
    public GameObject gameOver; // instaciar o game over criado na unity
    public List<GameObject> bananas; // minha lista de bananas na fase
    public bool isBananasEmpty = false; // se for true a lista esta vazia

    void Start() {
        instance = this; // assim eu posso chamar qualquer variavel nao privada em outras classes

        GameObject[] allBananas = GameObject.FindGameObjectsWithTag("Banana"); // achar todas as bananas da cena
        foreach (GameObject banana in allBananas) { // adicionar na lista de bananas todas as bananas da cena
            bananas.Add(banana);
        }
    }

    public void AtualizaScoreText() {
        scoreText.text = totalScore.ToString(); // pega o atributo text la do unity e altera (so aceita string)
    }

    public void GameOver() {
        gameOver.SetActive(true); // passa a deixar ativo a imagem de game over que antes estava invisivel
    }

    public void RestartGame() { // se der gameover volta para o comeco da fase
        SceneManager.LoadScene("Fase1");
    }

    public void CollectBananas(GameObject banana) {
        bananas.Remove(banana); // remove a banana da lista de bananas
        Debug.Log(bananas.Count);
    }

    public void BananasEmpty() { // verifica se a lista esta vazia
        if (bananas.Count == 0) {
            isBananasEmpty = true;
        }
    }

}
