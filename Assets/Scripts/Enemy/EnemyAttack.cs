using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 1;
    float timer;
    
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit " + other.gameObject);
        if (timer >= timeBetweenAttacks)
        {
            if (other.gameObject.tag == "PlayerBody")
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                timer = 0f;
            }
            if (other.gameObject.tag == "Shield")
            {                
                other.gameObject.GetComponent<PlayerShield>().TakeDamage(attackDamage);
                timer = 0f;
            }
        }
    }    
}

    

