using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public static EnemyHandler instance;

    public Transform waypointParent, spawnpointParent;
    WaveHandler waveHandler;

    public List <Vector2> spawnPoints;
    List<Animator> spawnDoors;

    public Transform centre;
    Vector2 centrePos;
    public static List<Vector2> waypoints;
    public static Vector2 lastPoint;
    public static Vector2 waypoint {
        get {
            Vector2 point;
            do
            {
                point = waypoints[Random.Range(0, waypoints.Count)];
            } while (point == lastPoint);
            lastPoint = point;
            return point;
        }
        private set { }
    }

    public ColorInfo [] colorMats;
    public ColorInfo colorInfo
    {
        get
        {
            int dice = Random.Range(0, 10);
            if (dice < 5)
            {
                return colorMats[0];
            } else
            {
                return colorMats[1];
            }
        }
    }

    private void Start()
    {
        centrePos = centre.position;
        waveHandler = FindObjectOfType<WaveHandler>();

        waypoints = new List<Vector2>();
        spawnPoints = new List<Vector2>();
        spawnDoors = new List<Animator>();
        foreach (Transform point in waypointParent)
        {
            waypoints.Add(point.position);
        }
        foreach (Transform point in spawnpointParent)
        {
            spawnPoints.Add(point.position);
            spawnDoors.Add(point.GetComponentInChildren<Animator>());
        }
        instance = this;

    }
    public void StartWave(Wave wave)
    {
        StartCoroutine("SpawnWave", wave);
    }
    IEnumerator SpawnWave(Wave wave)
    {
        List<GameObject> enemies = wave.enemies;
        float delay = wave.delay;
        int enemyIndex = 0;
        do
        {

            int spawnIndex = Random.Range(0, spawnPoints.Count);

            Vector2 spawnPoint = spawnPoints[spawnIndex];
            if (wave.isBoss)
                spawnPoint = centrePos;

            spawnDoors[spawnIndex].SetTrigger("Open");
            GameObject enemy = Instantiate(enemies[enemyIndex], spawnPoint, Quaternion.identity);
            waveHandler.AddEnemy(enemy);

            EnemyStats enemyScript = enemy.GetComponent<EnemyStats>();
            ColorInfo color = colorMats[Random.Range(0, colorMats.Length)];
            enemyScript.material = color.mat;
            enemyScript.tag = color.color;
            enemyIndex++;
            yield return new WaitForSeconds(delay/2);
            spawnDoors[spawnIndex].SetTrigger("Close");

            yield return new WaitForSeconds(delay-delay/3);
        } while (enemyIndex < enemies.Count);
    }

}
