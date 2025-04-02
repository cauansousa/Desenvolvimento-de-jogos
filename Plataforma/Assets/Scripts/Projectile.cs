using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Rigidbody2D rb2d;
    public Vector3 direction; // direcao do projetil -> tem que mudar la no unity e colocar a direcao que desejamos !!!!
    public float speed = 4f; // velocidade do projetil

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2.5f); // destroi o projetil depois de 2.5 sec
    }

    void Update() {
        this.transform.position += this.direction * this.speed * Time.deltaTime; // move o projetil
    }

    private void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.tag == "Player") { // quando atingir o player chama o game over e destroi o projetil
            GameController.instance.GameOver();
            Destroy(gameObject);
        }
    }

}
