using UnityEngine;

public class DeckControll : MonoBehaviour
{
    [SerializeField] 
    public GameObject Deck;
    private bool isActiveDeck = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Deck.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isActiveDeck)
            {
                Deck.SetActive(false);
                isActiveDeck = false;
            }
            else if (!isActiveDeck)
            {
                Deck.SetActive(true);
                isActiveDeck = true;
            }
            Debug.Log("It Work");
        }
    }
}
