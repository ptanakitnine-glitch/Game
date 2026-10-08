using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSelect : MonoBehaviour
{
    public PlaceTower placeTower;

    public GameObject squareButton;
    public GameObject circleButton;
    public GameObject capsuleButton;

    public GameObject squareHighlight;
    public GameObject circleHighlight;
    public GameObject capsuleHighlight;

    void Start()
    {
        squareButton.GetComponent<SpriteRenderer>().sortingOrder = 5;
        circleButton.GetComponent<SpriteRenderer>().sortingOrder = 5;
        capsuleButton.GetComponent<SpriteRenderer>().sortingOrder = 5;

        squareHighlight.GetComponent<SpriteRenderer>().sortingOrder = 4;
        circleHighlight.GetComponent<SpriteRenderer>().sortingOrder = 4;
        capsuleHighlight.GetComponent<SpriteRenderer>().sortingOrder = 4;

        ShowHighlight(squareHighlight);
    }

    void Update()
    {
   
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            placeTower.currentTower = placeTower.squareTower;
            ShowHighlight(squareHighlight);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            placeTower.currentTower = placeTower.circleTower;
            ShowHighlight(circleHighlight);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            placeTower.currentTower = placeTower.capsuleTower;
            ShowHighlight(capsuleHighlight);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            clickPos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.collider.gameObject == squareButton)
                {
                    placeTower.currentTower = placeTower.squareTower;
                    ShowHighlight(squareHighlight);
                }
                if (hit.collider.gameObject == circleButton)
                {
                    placeTower.currentTower = placeTower.circleTower;
                    ShowHighlight(circleHighlight);
                }
                if (hit.collider.gameObject == capsuleButton)
                {
                    placeTower.currentTower = placeTower.capsuleTower;
                    ShowHighlight(capsuleHighlight);
                }
            }
        }
    }

    void ShowHighlight(GameObject highlight)
    {
        squareHighlight.SetActive(false);
        circleHighlight.SetActive(false);
        capsuleHighlight.SetActive(false);
        highlight.SetActive(true);
    }
}