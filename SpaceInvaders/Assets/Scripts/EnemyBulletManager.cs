using UnityEngine;
using System.Collections.Generic;

public class EnemyBulletManager : MonoBehaviour
{
    public static EnemyBulletManager Instance; // Singleton

    private List<GameObject> activeBullets = new List<GameObject>(); // Lista de tiros ativos
    public int maxBullets = 2; // Número máximo de tiros na cena

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Verifica se um novo tiro pode ser criado
    public bool CanShoot()
    {
        return activeBullets.Count < maxBullets;
    }

    // Adiciona um tiro à lista
    public void RegisterBullet(GameObject bullet)
    {
        activeBullets.Add(bullet);
    }

    // Remove um tiro da lista quando ele é destruído
    public void UnregisterBullet(GameObject bullet)
    {
        activeBullets.Remove(bullet);
    }
}