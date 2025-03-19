using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public static Parallax Instance;

    private float lenght;
    public float movingSpeed = 5f;
    public GameObject cam;
    public float parallaxEffect;
    public float baseSpeed = 1f;    // Velocidade inicial
    public float maxSpeed = 10f;    // Velocidade máxima
    public float speedIncrement = 0.5f; // Incremento por pontuação

    private float currentSpeed;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update() {
        transform.position += Vector3.left * Time.deltaTime * movingSpeed * parallaxEffect;
        if(transform.position.x < -lenght ) {
            transform.position = new Vector3(lenght, transform.position.y, transform.position.z);
        }        
    }

    public void movingSpeedByScore(int score) // <-- Adicione "public"
    {
        // Sua lógica de aumento de velocidade aqui
        float newSpeed = 1 + (score / 100) * 0.5f; // Exemplo
        currentSpeed = Mathf.Clamp(newSpeed, baseSpeed, maxSpeed);
    }
}
