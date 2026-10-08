using UnityEditor;
using UnityEngine;
public class enemiesSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    public Health HP;
    private bool playSound = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.hitPoints <= 0 && !playSound)
        {
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
            audioSource.loop = false;
            playSound = true;
        }
    }
}
