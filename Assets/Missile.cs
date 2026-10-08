using UnityEngine;
public class Missile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject effect;

    [Header("Attributes")]
    [SerializeField] private float MissileSpeed = 5f;
    [SerializeField] private int MissileDamage = 3;
    [SerializeField] private float explosiveRadius = 2f;
    [SerializeField] private int explosiveDamage = 2;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * MissileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(MissileDamage);

        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, explosiveRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.tag == "enemy")
            {
                Damage(collider2D.gameObject);
                Instantiate(effect, transform.position, Quaternion.identity);
            }
        }
    }
    void Damage(GameObject enemy)
    {
        enemy.gameObject.GetComponent<Health>().TakeDamage(explosiveDamage);
    }
}
