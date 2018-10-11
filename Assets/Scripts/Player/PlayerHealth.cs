using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 10;    
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public static bool shielded;

    int currentHealth;
    bool damaged;    

    void Awake() {  
        currentHealth = playerHealth;
        shielded = false;
    }

    void Update()
    {        
        healthSlider.value = currentHealth;
        if (damaged) {
            damageImage.color = flashColour;
        } else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {              
        currentHealth -= amount;        
        damaged = true;
    }
    
    public int getHealth() {
        return currentHealth;
    }

    public void setHealth(int newHealth)
    {
        currentHealth = newHealth;
    }
    
}
