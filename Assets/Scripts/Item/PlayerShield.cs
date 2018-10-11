using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour {

    public int shieldHealth = 10;    
    public int currentShieldHealth;
    public float flashSpeed = 5f;
    public Slider bonusHealthSlider;
    public Image damageImage;
    public Color flashColour = new Color(0f, 0f, 1f, 0.1f);

    GameObject player;
    PlayerHealth playerHealth;

    EnemyHealth enemyHealth;

    bool damaged;
    float EffectTime;   
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerHealth = player.GetComponent<PlayerHealth>();

        GetComponent<Renderer>().sharedMaterial.SetFloat("_EffectTime", 0);        
    }

    void Update()
    {
        bonusHealthSlider.value = currentShieldHealth;
        screenFlash();
        shieldFlash();

        if (currentShieldHealth == 0)
        {            
            EffectTime -= Time.deltaTime * 4000;
            GetComponent<Renderer>().sharedMaterial.SetFloat("_EffectTime", EffectTime);

            damageImage.color = Color.clear;
            setAllCollidersStatus(false);
            Invoke("DestroyMe", 0.5f);
        }
        
    }

    void screenFlash()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    void shieldFlash()
    {
        if (EffectTime > 0 && currentShieldHealth > 0)
        {
            Vector4 v = new Vector4(0.7f, 1, 1, .2f);
            GetComponent<Renderer>().sharedMaterial.SetVector("_ShieldColor", v);

            EffectTime -= Time.deltaTime * 1000;
            GetComponent<Renderer>().sharedMaterial.SetFloat("_EffectTime", EffectTime);
        }
    }

    public void setAllCollidersStatus(bool active)
    {
        foreach(Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = active;
        }
    }

    void DestroyMe()
    {
        PlayerHealth.shielded = false;
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        damaged = true;        
        if (currentShieldHealth - amount < 0)
        {
            playerHealth.setHealth(playerHealth.getHealth()  - (amount - currentShieldHealth));
            currentShieldHealth = 0;
        }
        else
        {
            currentShieldHealth -= amount;
        }        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        bool hitEnemy = false;
        if (other.gameObject.tag == "Enemy")
        {
            hitEnemy = true;
        }      

        if (hitEnemy)
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                GetComponent<Renderer>().sharedMaterial.SetVector("_Position", transform.InverseTransformPoint(contact.point));
                EffectTime = 500;
                
                if(currentShieldHealth == 0)
                {
                    EffectTime = -1000;                    
                }
            }
        }
    }
}
