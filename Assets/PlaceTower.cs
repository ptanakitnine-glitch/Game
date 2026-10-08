using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceTower : MonoBehaviour
{
    public GameObject squareTower;
    public GameObject capsuleTower;
    public GameObject circleTower;

    public GameObject currentTower;

    void Start()
    {
        currentTower = squareTower;
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(mouseScreenPos, Vector2.zero);
            if (hit.collider == null)
            {
                Instantiate(currentTower, mouseScreenPos, Quaternion.identity);
            }
            Debug.Log($"Mouse Screen Position: {mouseScreenPos}");
        }
    }
}
