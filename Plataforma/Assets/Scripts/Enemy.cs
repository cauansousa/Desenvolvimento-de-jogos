using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Rigidbody2D rb2d;
    public Projectile Pineapple; // definir qual vai ser o projetil, tem que colcoar na no unity
    public float intervaloDeTiro = 2.0f; // de quanto em quanto tempo ele vai ficar atirando


    void Start() {
        rb2d = GetComponent<Rigidbody2D>();  
        InvokeRepeating("Shoot", 0f, intervaloDeTiro); // chama a funcao de atirar repetidamente a cada 2 sec (intervaloDeTiro)
    }

    void Update() {
        
    }

    private void Shoot() { // funcao para atirar
        Instantiate(this.Pineapple, this.transform.position, Quaternion.identity);
    }

}
