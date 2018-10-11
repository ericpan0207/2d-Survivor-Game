using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;

    void Start()
    {
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        Spawn();
    }

    void Spawn()
    {        
        if (playerHealth.getHealth() <= 0f)
        {
            Debug.Log("Player Dead");
            return;
        }
        float yCoord = Random.Range(-3.89f, 3.89f);
        float zCoord = 0f;
        float xCoord = Random.Range(-7.86f, 7.86f);
        Vector3 spawnLocation = new Vector3(xCoord, yCoord, zCoord);
        Instantiate(enemy, spawnLocation, Quaternion.identity);
        //Debug.Log("Spawned Enemy");
        //Debug.Log(enemies.Count);
    }
}
