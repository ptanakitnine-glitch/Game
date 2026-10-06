using UnityEngine;
using UnityEditor;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private Transform turretRotationPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private LayerMask enemyMask;
    private Transform target;
    private void Update() {
        if (target == null){
            FindTarget();
            return;
            {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
                    transform.position, 0f, enemyMask);
                if (hits.Length > 0)
                {
                    target = hits[0].transform;
                }
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
