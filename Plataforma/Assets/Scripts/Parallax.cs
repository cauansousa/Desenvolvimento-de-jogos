using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    private float length; // tamanho do sprite
    private float startPos; // posicao inicial
    private GameObject player; // instancia um player (variavel)
    public float parallaxEffect = 0.2f; // velocidade do parallax quando o personagem anda
    public float baseSpeed = 0.3f; // velocidade padrao do parallax
    private float lastPlayerX; // ultimo valor de X do player
    private float playerSpeed; // velocidade do personagem

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player"); // procura o player pela sua Tag

        startPos = transform.position.x; // pega posicao inicial
        length = GetComponent<SpriteRenderer>().bounds.size.x; // pega o tamanho do sprite
        lastPlayerX = player.transform.position.x; // pega ultima posicao do player
    }

    void Update() {
        playerSpeed = (player.transform.position.x - lastPlayerX) / Time.deltaTime; // calcula velocidade do player
        lastPlayerX = player.transform.position.x; // pega ultima posicao do player

        float parallaxSpeed = baseSpeed + (playerSpeed * parallaxEffect); // define a velocidade do fundo (mínima + influência do movimento do player)

        transform.position += Vector3.left * parallaxSpeed * Time.deltaTime; // move o fundo na direção contrária ao movimento do player

        if (transform.position.x < startPos - length) { // reposiciona o fundo do background quando acabar ou sair
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
