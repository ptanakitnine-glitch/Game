using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public GameObject Ending;
    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int waveHeart=5;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 400;
    }

    private void Update()
    {
        if (waveHeart<=0)
        {
            Ending.SetActive(true);
        }
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough");
            return false;
        }
    }
    public void decreasewaveHeart()
    {
        waveHeart -= 1;
        
    }
    public void restartScene()
    {
        SceneManager.LoadScene(0);
        Debug.Log("restart");
    }
}
