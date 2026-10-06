using UnityEngine;
using UnityEditor;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private Transform turretRotatiomPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;

    private Transform target;
    private void Update()
    {
        if (target == null)
        {
            FindTarget() {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2))
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
