using UnityEngine;
using UnityEditor;
public class Knight : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMark;
    [SerializeField] private GameObject SwordSlashPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Animator myAnimator;


    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
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
        GameObject SwordSlashObj = Instantiate(SwordSlashPrefab, firingPoint.position, Quaternion.identity);
        SwordSlash SworadSlashScript = SwordSlashObj.GetComponent<SwordSlash>();
        SworadSlashScript.SetTarget(transform);
    }

    private void Findtarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMark);
        float distance = 999;
        foreach (RaycastHit2D hit in hits)
        {
            if (distance > hit.transform.gameObject.GetComponent<EnemyMovement>().calDis())
            {
                target = hit.transform;
                distance = hit.transform.gameObject.GetComponent<EnemyMovement>().calDis();
            }
        }
    }
        private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}