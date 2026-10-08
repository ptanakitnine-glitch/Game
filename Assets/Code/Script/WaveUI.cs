using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;

    private void Update()
    {
        waveText.text = " " + enemySpawner.currentWave;
    }
}