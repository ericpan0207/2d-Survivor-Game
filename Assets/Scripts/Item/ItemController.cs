using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

    public GameObject shield;
    public int healAmount = 5;

    GameObject player;    
    GameObject playerBody;    
    PlayerHealth playerHealth;    
    SpriteRenderer itemRender;
    PlayerShield playerShieldScript;
    IEnumerator coroutine;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody");       
        
        playerHealth = playerBody.GetComponent<PlayerHealth>();
        itemRender = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        coroutine = Flash();
        StartCoroutine(coroutine);
    }
        
    IEnumerator Flash()
    {
        int count = 0;
        yield return new WaitForSeconds(4f);
        while (true)
        {            
            for (float f = 1f; f >= 0; f -= 0.1f)
            {
                Color c = itemRender.color;
                c.a = f;
                itemRender.color = c;
                yield return new WaitForSeconds(.02f);
            }

            for (float f = 0f; f <= 1; f += 0.1f)
            {
                Color c = itemRender.color;
                c.a = f;
                itemRender.color = c;
                yield return new WaitForSeconds(.02f);
            }
            count++;
            if (count == 3)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            if (gameObject.tag == "HealthPack")
            {
                if (playerHealth.getHealth() + healAmount > playerHealth.playerHealth)
                {
                    playerHealth.setHealth(playerHealth.playerHealth);
                } else
                {
                    playerHealth.setHealth(playerHealth.getHealth() + healAmount);
                }
                //playerHealth.currentHealth = Mathf.Min(playerHealth.startingHealth, playerHealth.currentHealth + healAmount);
            }
            else if (gameObject.tag == "ShieldItem")
            {
                bool created = false;
                if (GameObject.FindGameObjectWithTag("Shield") == null)
                {
                    PlayerHealth.shielded = true;
                    Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                    var playerShield = Instantiate(shield, playerPos, Quaternion.identity);
                    playerShield.transform.parent = player.transform;
                    created = true;
                }
                shield = GameObject.FindGameObjectWithTag("Shield");                
                playerShieldScript = shield.GetComponent<PlayerShield>();
                if(!created && playerShieldScript.currentShieldHealth == 0)
                {
                    playerShieldScript.CancelInvoke("DestroyMe");
                    playerShieldScript.setAllCollidersStatus(true);                    
                }
                playerShieldScript.currentShieldHealth = playerShieldScript.shieldHealth;                
            }
            Destroy(gameObject, 0f);
        }
    }
}
