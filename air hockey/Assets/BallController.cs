using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float maxSpeed = 25f; // Velocidade máxima ajustável no Inspector
    private float lastWallHitTime;

    public AudioSource source;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Melhora detecção
        Invoke("LaunchBall", 2);
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // Limita a velocidade máxima
        if (rb2d.linearVelocity.magnitude > maxSpeed)
        {
            rb2d.linearVelocity = rb2d.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        source.Play();
        if (coll.collider.CompareTag("Wall"))
        {
            if (Time.time - lastWallHitTime < 0.1f) return;
        
            lastWallHitTime = Time.time;
            Vector2 normal = coll.contacts[0].normal;
            Vector2 reflectedVelocity = Vector2.Reflect(rb2d.linearVelocity, normal);

            reflectedVelocity += normal * 3f;
            rb2d.linearVelocity = reflectedVelocity;
        }
    }

    public void LaunchBall() // Método público e sem parâmetros
    {
        float rand = Random.Range(0, 2);
        Vector2 force = (rand < 1) ? new Vector2(-15, -20) : new Vector2(-15, 20);
        rb2d.AddForce(force);
    }

    public void ResetPosition()
    {
        rb2d.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void Restart()
    {
        ResetPosition();
        Invoke("LaunchBall", 1);
    }


}