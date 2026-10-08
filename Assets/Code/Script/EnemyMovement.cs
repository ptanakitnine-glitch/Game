using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    private SpriteRenderer spriteRenderer;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length) // end of path list
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } 
            else
            {
                target = LevelManager.main.path[pathIndex]; // new target
            }
        }
    }
    private void FixedUpdate() // move
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        Debug.Log(direction);
        if (direction.x > 0.5)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x < -0.5)
        {
            spriteRenderer.flipX = false;
        }
    }
}
