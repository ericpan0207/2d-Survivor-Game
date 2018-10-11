using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {

    public int attackDamage = 1;

    GameObject player;
    Rigidbody2D playerRB;
    Rigidbody2D objectRigidbody;
    public float speed = 1;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        objectRigidbody = transform.GetComponent<Rigidbody2D>();
        Vector2 projVel = transform.up * speed;
        if ((projVel + playerRB.velocity).magnitude > projVel.magnitude)
        {
            objectRigidbody.velocity = projVel + playerRB.velocity;
        }
        else
        {
            objectRigidbody.velocity = projVel;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);            
        }
        gameObject.SetActive(false);        
    }
}
