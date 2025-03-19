using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public KeyCode moveUp = KeyCode.W;      // Move a raquete para cima
    public KeyCode moveDown = KeyCode.S;    // Move a raquete para baixo
    public Projetil laserPrefab;
    public float speed = 10.0f;             // Define a velocidade da raquete
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete
    public float boundY = 3.4f;
    private float lastShootTime = 0.0f;
    private int life = 3;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();  
    }

    void Update() {
        Mover();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShootTime + 0.3f) { 
            Atirar();
            lastShootTime = Time.time;  // Atualiza o tempo do último disparo
        }
    }

    void Mover () {

        var vel = rb2d.velocity;                // Acessa a velocidade da raquete
        if (Input.GetKey(moveUp)) {             // Velocidade da Raquete para ir para cima
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown)) {      // Velocidade da Raquete para ir para cima
            vel.y = -speed;                    
        }
        else {
            vel.y = 0;                          // Velociade para manter a raquete parada
        }
        rb2d.velocity = vel;                    // Atualizada a velocidade da raquete

        var pos = transform.position;           // Acessa a Posição da raquete
        if (pos.y > boundY) {                  
            pos.y = boundY;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.y < -boundY) {
            pos.y = -boundY;                    // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
        }
        transform.position = pos;               // Atualiza a posição da raquete

    }

    private void Atirar() {
        Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Laser")) {
            life--;
            if (life == -1) {
                Destroy(this.gameObject);
                // Game Over
            }
        }
    }
    

}