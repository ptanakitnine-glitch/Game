using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject[] enemyPrefabs;

    [Header("Attributes")]
    public int baseEnemies = 3;
    public float enemiesPerSecond = 0.5f;
    public float timeBetweenWaves = 5f;
    public float difficultyScalingFactor = 1.2f;
    public float enemiesPerSecondCap = 5f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private float eps; //enemies per sec
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    public LevelManager levelManager;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        StartWave();
    }
    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    private void StartWave()
    {
        
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        if (levelManager.waveHeart > 0)
        {
            isSpawning = false;
            timeSinceLastSpawn = 0f;
            currentWave++;
            StartWave();
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(1, 100);
        if (index < 2)
        {
            index = 2;
        }
        else if (index < 39.5)
        {
            index = 1;
        }
        else
        {
            index = 0;
        }
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor),0f,enemiesPerSecondCap);
    }
}
