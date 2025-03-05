using UnityEngine;

public class Invasor : MonoBehaviour
{
    public Sprite[] animacaoSprites; // Array de sprites para animação
    public float tempoAnimacao = 1.0f; // Tempo entre frames da animação
    public GameObject bulletPrefab; // Prefab do projétil inimigo
    public float minShootInterval = 0.5f; // Intervalo mínimo entre tiros (0.5s = 2 tiros por segundo)
    public float maxShootInterval = 1.0f; // Intervalo máximo entre tiros

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame; // Frame atual da animação
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 1.0f;
    private float speed = 1.0f;
    private float nextShootTime;

    private static float limiteDireita = 8.0f;
    private static float limiteEsquerda = -8.0f;
    private static float distanciaDescida = (-0.5f / 3);
    private static bool mudarDirecao = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Inicia a animação
        InvokeRepeating(nameof(AnimateSprite), this.tempoAnimacao, this.tempoAnimacao);

        // Configura a velocidade inicial
        rb2d = GetComponent<Rigidbody2D>();
        var vel = rb2d.velocity;
        vel.x = speed; // Começa indo para a direita
        rb2d.velocity = vel;

        // Define o próximo tempo de tiro
        SetNextShootTime();
    }

    private void AnimateSprite()
    {
        _animationFrame++;

        if (_animationFrame >= this.animacaoSprites.Length)
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animacaoSprites[_animationFrame];
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Verifica os limites e muda a direção
        if (timer >= waitTime)
        {
            VerificarLimite();
            timer = 0.0f;
        }

        // Descida dos invasores
        if (mudarDirecao)
        {
            DescerENovamente();
        }

        // Tiro aleatório
        if (Time.time >= nextShootTime)
        {
            Shoot();
            SetNextShootTime();
        }
    }

    void VerificarLimite()
    {
        if (transform.position.x >= limiteDireita || transform.position.x <= limiteEsquerda)
        {
            mudarDirecao = true; // Qualquer invasor que tocar uma das bordas, muda direção
        }
    }

    void DescerENovamente()
    {
        mudarDirecao = false;

        Invasor[] invasores = FindObjectsOfType<Invasor>();

        foreach (Invasor invasor in invasores)
        {
            var vel = invasor.rb2d.velocity;
            vel.x *= -1; // Inverte a direção de X
            invasor.rb2d.velocity = vel;

            // Invasores descem juntos
            invasor.transform.position = new Vector2(invasor.transform.position.x, invasor.transform.position.y + distanciaDescida);
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab não foi atribuído no Inspector!");
            return;
        }

        // Verifica se pode atirar (não há mais de 2 tiros na cena)
        if (EnemyBulletManager.Instance.CanShoot())
        {
            // Instancia o projétil na posição do invasor
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

    void SetNextShootTime()
    {
        // Define o próximo tempo de tiro aleatoriamente
        nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }
}