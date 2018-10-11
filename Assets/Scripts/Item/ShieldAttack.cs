using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour {

    public int attackDamage = 1;
    public float timeBetweenAttacks = 0.5f;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && timer >= timeBetweenAttacks)
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
}
