using UnityEngine;

public class Banana : MonoBehaviour {

    private SpriteRenderer sr; // é onde desativa o objeto da cena, ele ainda ta la, mas nao visivel
    private BoxCollider2D box; // colisao
    public GameObject collected; // instancia nossa animacao de coletar banana
    public int score = 10; // vai ser nossa pontuacao de bananas pegas

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();

    }

    void OnTriggerEnter2D(Collider2D collider) { // trigger eh quando o personagem pode passar por cima no objeto (se fosse uma bola nao teria trigger por exemplo)
        if (collider.gameObject.tag == "Player") {
            sr.enabled = false; // deixa a banana nao visivel na cena
            box.enabled = false; // desativa a colisao
            collected.SetActive(true); // deixa visivel a animacao de coletar a banana

            GameController.instance.totalScore += score; // aumenta o score la na tela de jogo
            GameController.instance.AtualizaScoreText(); // chama a funcao de atualizar a score

            GameController.instance.CollectBananas(gameObject); // chama minha funcao para atualizar a lista de bananas
            GameController.instance.BananasEmpty(); // chama a funcao para saber se tem 0 bananas na lista

            Destroy(gameObject, 0.1f); // destruir o próprio objeto -> banana depois de 1sec

        }

    }

}
