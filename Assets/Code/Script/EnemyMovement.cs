using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    public int prog = 0;
    private LevelManager lm;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
        lm = FindAnyObjectByType<LevelManager>();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if(pathIndex == lm.path.Length) // end of path list
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else
            {
                target = lm.path[pathIndex]; // new target
            }
        }
    }
    private void FixedUpdate() // move
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
    public float calDis()
    {
        float maxDis = 999;
        float distance = 0;
        Vector3 CurrPos = this.transform.position;
        for (int i = pathIndex + 1; i < lm.path.Length-1; i++)
        {
            distance += Vector2.Distance(lm.path[i].position, lm.path[i+1].position);
        }
        distance += Vector2.Distance(CurrPos, lm.path[pathIndex].position);
        return distance;
    }
}
