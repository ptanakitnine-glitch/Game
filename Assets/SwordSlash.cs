using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float swordSpeed = 5f;
    [SerializeField] private float swingRadius = 2f;
    [SerializeField] private int swingDamage = 2;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * swordSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Explode();
        Destroy(gameObject);
    }
    void Explode()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, swingRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.tag == "enemy")
            {
                Damage(collider2D.gameObject);
            }
        }
    }
    void Damage(GameObject enemy)
    {
        enemy.gameObject.GetComponent<Health>().TakeDamage(swingDamage);
    }
}