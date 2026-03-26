using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [Header("WaveSettings")]
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private Transform  spawnPoints;
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
        int EnemiesToSpawn = enemiesPerWave + (currentWave - 1) * 2;
        enemiesAlive = EnemiesToSpawn;
        waveInProgress = true;

        

   
    }
   
   
   
   
   
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
