using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public List<Wave> waves;

    public List <GameObject> activeEnemies;

    EnemyHandler enemyHandler;

    public TextMeshProUGUI waveCounter;
    int waveCount;

    private void Start()
    {
        enemyHandler = FindObjectOfType<EnemyHandler>();
        StartCoroutine("spawnFirst");
        

    }
    IEnumerator spawnFirst()
    {
        yield return new WaitForSeconds(1);
        StartNextWave();
    }
    void StartNextWave()
    {
        if (waves.Count == 0)
        {
            waveCounter.text = "stage complete";
            return;
        }
        enemyHandler.StartWave(waves[0]);
        waves.RemoveAt(0);
        waveCount = waves.Count;
        if (waveCount > 1)
        {
            waveCounter.text = waveCount + " waves left";
        } else if (waveCount == 1)
        {
            waveCounter.text = "next wave is boss";
        } 
    }
    public void AddEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }
    public void EnemyDeath(GameObject enemy)
    {
        Debug.Log(activeEnemies.Count);
        activeEnemies.Remove(enemy);
        if (activeEnemies.Count == 0)
        {
            StartNextWave();
        }
        //activeEnemiesactiveEnemies.Find(a => a == enemy);
    }
}
