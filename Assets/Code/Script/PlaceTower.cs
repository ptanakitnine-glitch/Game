using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceTower : MonoBehaviour
{
    public GameObject squareTower;
    public GameObject capsuleTower;
    public GameObject circleTower;
    public LevelManager levelManager;

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
                if(currentTower == squareTower && levelManager.currency >= 150)
                {
                    levelManager.currency -= 150;
                    Instantiate(currentTower, mouseScreenPos, Quaternion.identity);
                }
                if (currentTower == capsuleTower && levelManager.currency >= 350)
                {
                    levelManager.currency -= 350;
                    Instantiate(currentTower, mouseScreenPos, Quaternion.identity);
                }
                if (currentTower == circleTower && levelManager.currency >= 500)
                {
                    levelManager.currency -= 500;
                    Instantiate(currentTower, mouseScreenPos, Quaternion.identity);
                }
            }
            Debug.Log($"Mouse Screen Position: {mouseScreenPos}");
        }
    }
}
