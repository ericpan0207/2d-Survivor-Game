using UnityEngine;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject healthBarBackground;
    
    public int startingHealth = 1;
    public int scoreValue = 10;
    int currentHealth;

    GameObject childObject;
    GameObject childObject2;
    
    void Start()
    {
        Vector3 spawn = transform.position + new Vector3(0, .3f, 0);
        childObject = Instantiate(healthBar, spawn, Quaternion.identity) as GameObject;
        childObject2 = Instantiate(healthBarBackground, spawn, Quaternion.identity) as GameObject;
        //childObject.transform.parent = this.transform;
        //childObject2.transform.parent = this.transform;
        currentHealth = startingHealth;        
    }

    private void Update()
    {
        childObject.transform.position = transform.position + new Vector3(0, .3f, 0);
        childObject2.transform.position = transform.position + new Vector3(0, .3f, 0);
    }

    public void TakeDamage(int amount)
    {             
        currentHealth -= amount;
        float healthChange = Mathf.Min(amount * 0.12f / startingHealth, 0.12f);
        childObject.transform.localScale -= new Vector3(healthChange, 0, 0);
        if (currentHealth <= 0)
        {
            MultiplierEffect.enemyDied = true;
            MultiplierEffect.tempScore += scoreValue;
            //UIManager.score += scoreValue;
            //healthBar.transform.localScale = new Vector3(0.12f, 0.12f, 1);
            Destroy(childObject);
            Destroy(childObject2);
            Destroy(gameObject);
        }
    }

    public int getHealth()
    {
        return currentHealth;
    }
       
}
