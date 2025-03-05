using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab do tiro
    public float minShootInterval = 200f; // Intervalo mínimo entre tiros
    public float maxShootInterval = 200f; // Intervalo máximo entre tiros

    private float nextShootTime;

    void Start()
    {
        // Define o próximo tempo de tiro
        SetNextShootTime();
    }

    void Update()
    {
        // Verifica se é hora de atirar
        if (Time.time >= nextShootTime + 10f)
        {
            Shoot();
            SetNextShootTime();
        }
    }

    void Shoot()
    {
        // Instancia o tiro na posição do inimigo
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    void SetNextShootTime()
    {
        // Define o próximo tempo de tiro aleatoriamente
        nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }
}