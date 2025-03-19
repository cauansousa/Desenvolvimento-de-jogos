using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro; // Importante para usar o TextMeshPro

public class Projetil : MonoBehaviour {

    private Rigidbody2D rb2d;
    public Vector3 direction;
    public float speed;

    // Referência para o texto de pontuação (TextMeshPro)

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Barreira")) {
            Destroy(gameObject);
            //Debug.Log("AAAAAAAAAAAAA");

        }
        else if (other.CompareTag("Enemy")) {
            Destroy(gameObject); // Destruir o projétil
            //Destroy(other.gameObject); // Destruir o invasor
            Debug.Log("PEGOU");
        }

    }

    private void Update() {
        this.transform.position += this.direction * this.speed * Time.deltaTime; // move o projetil
    }

}
