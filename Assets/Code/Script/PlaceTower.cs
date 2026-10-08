using UnityEngine;
using UnityEngine.InputSystem;
public class PlaceTower : MonoBehaviour
{
    public GameObject squareTower;
    public GameObject triangleTower;
    public GameObject circleTower;

    GameObject currentTower;
    void Start()
    {
        currentTower = squareTower;
    }

    // Update is called once per frame
    void Update()

    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTower = squareTower;
            Debug.Log("Square selected");
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTower = triangleTower;
            Debug.Log("Triangle selected");
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTower = circleTower;
            Debug.Log("Circle selected");
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(mouseScreenPos, Vector2.zero);
            if (hit.collider == null)
            {
                Instantiate(currentTower, mouseScreenPos, Quaternion.identity);
            }
            Debug.Log($"Mouse Screen Position; {mouseScreenPos}");
        }
    }
}