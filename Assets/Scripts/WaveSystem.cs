using System.Collections;
using UnityEngine;
using TMPro; 

public class WaveSystem : MonoBehaviour
{
    [Header("WaveSettings")]
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float spawnTime = 4f;
    [SerializeField] private TextMeshProUGUI waveText; 

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;
    private const float StartWave = 3;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(StartWave);
        currentWave++;
        StartCoroutine(ShowMsg($"Wave {currentWave} starting"));

        int totalToSpawn = enemiesPerWave + (currentWave - 1) * 2;
        enemiesAlive = 0;
        waveInProgress = true;
        for (int i = 0; i < totalToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void SpawnEnemy()
    {
        enemiesAlive++;
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

            StartCoroutine(ShowMsg($"Wave {currentWave} cleared!"));
            StartCoroutine(StartNextWave());
        }
    }
    IEnumerator ShowMsg(string msg)
    {
        waveText.text = msg;
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        waveText.gameObject.SetActive(false);
    }
}