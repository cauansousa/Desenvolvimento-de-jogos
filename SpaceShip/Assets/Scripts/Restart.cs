using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndGame : MonoBehaviour {

void OnGUI () {

    GUIStyle style = new GUIStyle();
    style.fontSize = 50;  // Define o tamanho da fonte
    style.alignment = TextAnchor.MiddleCenter;  // Alinha o texto no centro
    if (GUI.Button(new Rect(Screen.width / 2 - 60, 350, 120, 53), "RESTART")) {
        //Projetil.pontuacao = 0;
        SceneManager.LoadScene("Game");
    }

    // Exibe o texto com o novo estilo
    // GUI.Label(new Rect(Screen.width / 2 - 100, 250, 200, 100), "GAME END", style);

}

}