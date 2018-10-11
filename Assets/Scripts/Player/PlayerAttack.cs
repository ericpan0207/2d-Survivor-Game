using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    
    public int attackDamage = 1;    
    public float bulletSpeed = 1f;
    
    float spawnDistance = 0.3f;
    

    void Update ()
    {    
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Fire();            
        }
    }

    //Projectile Launch
    void Fire()
    {
        //Shoots where touched
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Vector2 dir = touchPos - (new Vector2(transform.position.x, transform.position.y));
        dir.Normalize();        
        
        Vector2 playerPos = transform.position;
        spawnDistance = PlayerHealth.shielded ? 0.5f : 0.3f;        
        Vector2 spawnpos = playerPos + dir * spawnDistance;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion projectileRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = spawnpos;
            bullet.transform.rotation = projectileRotation;
            bullet.SetActive(true);
        }        
    }

    //Front Attack
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
}
