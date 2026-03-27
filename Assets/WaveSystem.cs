using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [Header("WaveSettings")]
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float spawnTime = 1f;


    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(5);


        currentWave++;
        Debug.Log("Wave " + currentWave + " starting!");


        int EnemiesToSpawn = enemiesPerWave + (currentWave - 1) * 2;
        enemiesAlive = EnemiesToSpawn;
        waveInProgress = true;


        for (int i = 0; i < enemiesAlive; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }


    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemy1Prefab, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<EnemyHealth>().SetWaveManager(this);
    }

    public void OnEnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0 && waveInProgress)
        {
            waveInProgress = false;
            Debug.Log("Wave " + currentWave + " cleared!");
            StartCoroutine(StartNextWave());
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
