using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static int score;
    public static int highScore;

    GameObject player;    
    public Text healthText;
    public Text healthValue;
    public Text shieldValue;
    public Text currentScore;
    public Text hScore;    
    public Slider bonusHSlider;
    public Image damageImage;

    GameObject shield;
    PlayerHealth playerHealth;
    PlayerShield playerShield;    

    //float timer = 0f;   

    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        healthText.text = "Health";
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update ()
    {
        if (playerHealth.getHealth() > 0)
        {
            updateHealth();
            //timer += Time.deltaTime;
            //if ((int)Mathf.Round(timer) == 1)
            //{
            //    score += 1;
            //    timer = 0;
            //}           
        }
        updateScore();

    }

    void updateHealth()
    {
        healthValue.text = playerHealth.getHealth() + "/" + playerHealth.playerHealth;
        if (GameObject.FindGameObjectWithTag("Shield") != null)
        {
            shield = GameObject.FindGameObjectWithTag("Shield");
            playerShield = shield.GetComponent<PlayerShield>();
            playerShield.bonusHealthSlider = bonusHSlider;
            playerShield.damageImage = damageImage;

            shieldValue.text = playerShield.currentShieldHealth + "/" + playerShield.shieldHealth;
        }
        else
        {
            shieldValue.text = "";
        }
    }

    void updateScore()
    {
        currentScore.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
        }
        hScore.text = "High Score: " + highScore;        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
