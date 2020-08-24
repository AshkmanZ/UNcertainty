using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject[] bonuses;
    public float delay = 20;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        SpawnShit();
        yield return new WaitForSeconds(Random.Range(delay - 2, delay + 6));
        StartCoroutine(Start());
    }

    void SpawnShit()
    {
        int index = Random.Range(0, bonuses.Length);
        Instantiate(bonuses[index], EnemyHandler.waypoint, Quaternion.identity);
    }
}
