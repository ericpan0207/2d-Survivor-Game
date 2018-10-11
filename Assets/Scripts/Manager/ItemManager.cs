using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public PlayerHealth playerHealth;
    public GameObject healthPack;
    public GameObject shield;   
    public float spawnTimeMin;
    public float spawnTimeMax;

    void Start() {
        float spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        InvokeRepeating("SpawnHealth", spawnTime, spawnTime);
        InvokeRepeating("SpawnShield", spawnTime, spawnTime + 2);
    }  

    void SpawnHealth() {   
        if (playerHealth.getHealth() <= 0)
        {
            return;
        }
        float yCoord = Random.Range(-3.89f, 3.89f);
        float zCoord = 0f;
        float xCoord = Random.Range(-7.86f, 7.86f);
        Vector3 spawnLocation = new Vector3(xCoord, yCoord, zCoord);

        Instantiate(healthPack, spawnLocation, Quaternion.identity);                
    }

    void SpawnShield()
    {
        if (playerHealth.getHealth() <= 0)
        {
            return;
        }
        float yCoord = Random.Range(-3.89f, 3.89f);
        float zCoord = 0f;
        float xCoord = Random.Range(-7.86f, 7.86f);
        Vector3 spawnLocation = new Vector3(xCoord, yCoord, zCoord);

        Instantiate(shield, spawnLocation, Quaternion.identity);
     }
}
