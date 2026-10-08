using UnityEngine;
public class Missile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float MissileSpeed = 5f;
    [SerializeField] private int MissileDamage = 3;
    [SerializeField] private float explosiveRadius = 2f;

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

        if (explosiveRadius > 0f)
        {
            else
            {

            }
        }


        Destroy(gameObject);
    }
}
