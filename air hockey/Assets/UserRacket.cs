using UnityEngine;

public class UserRacket : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject wall;
    private Vector2 movementBoundsMin;
    private Vector2 movementBoundsMax;
    private Vector2 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        initialPosition = transform.position;


        wall = GameObject.FindWithTag("Field");
        if (wall != null)
        {
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            Vector3 wallSize = wallRenderer.bounds.size;
            Vector3 wallPosition = wall.transform.position;

            movementBoundsMin = new Vector2(
                wallPosition.x - wallSize.x / 2,
                wallPosition.y - wallSize.y / 2
            );

            movementBoundsMax = new Vector2(
                wallPosition.x + wallSize.x / 2,
                wallPosition.y
            );
        }
    }


    public void ResetPosition()
    {
    // Configure a posição inicial da raquete
    GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    transform.position = new Vector2(-3, 0);
    
    // Exemplo para posição fixa:
    // transform.position = new Vector2(7.5f, 0); 
    }

    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = new Vector2(
            Mathf.Clamp(mousePos.x, movementBoundsMin.x, movementBoundsMax.x),
            Mathf.Clamp(mousePos.y, movementBoundsMin.y, movementBoundsMax.y)
        );

        rb.MovePosition(targetPosition); 
    }
}