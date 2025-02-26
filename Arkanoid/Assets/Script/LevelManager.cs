using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int totalBricks;

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

    void Start()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    public void BrickDestroyed()
    {
        totalBricks--;
        if (totalBricks <= 0)
        {
            SceneLoader.Instance.IrParaProximoNivel();
        }
    }
}