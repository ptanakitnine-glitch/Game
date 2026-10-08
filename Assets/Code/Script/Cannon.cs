using UnityEngine;
using UnityEditor;
public class Cannon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMark;
    [SerializeField] private GameObject MissilePrefab;
    [SerializeField] private Transform firingPoint;



    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null)
        {
            Findtarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }
    private void Shoot()
    {
        GameObject MissileObj = Instantiate(MissilePrefab, firingPoint.position, Quaternion.identity);
        Bullet MissileScript = MissileObj.GetComponent<Bullet>();
        Findtarget();
        MissileScript.SetTarget(target);
    }

    private void Findtarget()
    {
        target = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMark);
        float bestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            EnemyMovement enemy = hit.GetComponentInParent<EnemyMovement>();
            if (enemy == null) continue;

            float currentDistance = enemy.calDis();
            if (currentDistance < bestDistance)
            {
                bestDistance = currentDistance;
                target = enemy.transform;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 180f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed + Time.deltaTime);
    }

}
