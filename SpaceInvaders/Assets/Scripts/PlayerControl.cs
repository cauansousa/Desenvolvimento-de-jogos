using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public KeyCode moveLeft = KeyCode.A;      // Move a raquete para cima
    public KeyCode MoveRight = KeyCode.D;    // Move a raquete para baixo
    public Projetil laserPrefab;
    public float speed = 10.0f;             // Define a velocidade da raquete
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete
    private float lastShootTime = 0.0f;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();  
    }

    void Update() {
        Mover();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShootTime + 0.1f) { 
            Atirar();
            lastShootTime = Time.time;  // Atualiza o tempo do último disparo
        }
    }

    void Mover () {

        var vel = rb2d.velocity;                // Acessa a velocidade da raquete
        if (Input.GetKey(moveLeft)) {             // Velocidade da Raquete para ir para cima
            vel.x = -speed;
        }
        else if (Input.GetKey(MoveRight)) {      // Velocidade da Raquete para ir para cima
            vel.x = speed;                    
        }
        else {
            vel.x = 0;                          // Velociade para manter a raquete parada
        }
        rb2d.velocity = vel;                    // Atualizada a velocidade da raquete

        var pos = transform.position;           // Acessa a Posição da raquete
        if (pos.x > 8f) {
            pos.x = 8f;
        }
        else if (pos.x < -8f) {
            pos.x = -8f;
        }
        transform.position = pos;               // Atualiza a posição da raquete
    }

    private void Atirar() {
        Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
    }
    

}