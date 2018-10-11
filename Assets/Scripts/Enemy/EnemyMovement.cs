using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void Update()
    {
        if (enemyHealth.getHealth() > 0 && playerHealth.getHealth() > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }        
    }
}
