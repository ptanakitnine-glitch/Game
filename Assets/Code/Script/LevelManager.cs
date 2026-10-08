using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public GameObject Ending;
    public Transform startPoint;
    public Transform[] path;
    public AudioSource audioSource;
    public AudioClip bgmEnd;
    public bool playSound = false;

    public int currency;
    public int waveHeart = 5;

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
        if (waveHeart<=0 && !playSound)
        {
            Ending.SetActive(true);
            audioSource.Stop();
            audioSource.clip = bgmEnd;
            audioSource.Play();
            audioSource.loop = false;
            playSound = true;
        }
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            currency += 1000;
        }
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            waveHeart += 10;
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
