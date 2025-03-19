using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    public float velocidade = 5.0f; // Velocidade do movimento
    public float boundY = 3.2f; // Limite de movimento no eixo Y
    private int direcao = -1; // Começa movendo para baixo
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete
    public EnemyProjetil laserPrefab;
    public float intervaloDeTiro = 1.0f;
    private int score = 0;



    // Start is called before the first frame update
    void Start() {

        rb2d = GetComponent<Rigidbody2D>();  
        InvokeRepeating("Atirar", 0f, intervaloDeTiro);
     
    }

    // Update is called once per frame
    void Update() {

        Mover();

    }

    void Mover() {

        transform.position += new Vector3(0, direcao * velocidade * Time.deltaTime, 0);

        // Verifica se atingiu o limite e inverte a direção
        if (transform.position.y <= -boundY) {
            direcao = 1; // Muda para cima
        }

        else if (transform.position.y >= boundY) {
            direcao = -1; // Muda para baixo
        }

    }

    void Atirar() {
        Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Laser")) {
            score += 10;
            Parallax.Instance.movingSpeedByScore(score);
        }
    }
}
