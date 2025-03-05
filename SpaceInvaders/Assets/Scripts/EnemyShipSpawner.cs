using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public GameObject enemyShipPrefab;  
    public float minSpawnInterval = 5f; 
    public float maxSpawnInterval = 15f;
    public float fixedY = 4f; // Posição Y fixa

    void Start()
    {
        Invoke("SpawnEnemyShip", Random.Range(0f, 2f));
    }

    void SpawnEnemyShip()
    {
        if (enemyShipPrefab == null)
        {
            Debug.LogError("Prefab da nave não foi atribuído!");
            return;
        }

        // Escolhe direção aleatória
        float direction = (Random.Range(0, 2) == 0) ? 1f : -1f;

        // Posição inicial (fora da tela, Y fixo)
        Vector2 spawnPosition = new Vector2(
            (direction == 1f) ? -12f : 12f, 
            fixedY
        );

        // Instancia a nave
        GameObject ship = Instantiate(enemyShipPrefab, spawnPosition, Quaternion.identity);
        
        // Configura a direção e velocidade
        EnemyShipMovement movement = ship.GetComponent<EnemyShipMovement>();
        movement.SetDirection(direction);
        movement.horizontalSpeed = Random.Range(4f, 8f); // Velocidade aleatória

        // Inverte o sprite se necessário
        if (direction == -1f)
        {
            ship.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // Próximo spawn
        Invoke("SpawnEnemyShip", Random.Range(minSpawnInterval, maxSpawnInterval));
    }
}