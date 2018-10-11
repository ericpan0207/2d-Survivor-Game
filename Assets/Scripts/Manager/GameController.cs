using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject button;
    GameObject player;
    PlayerHealth playerHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerHealth = player.GetComponent<PlayerHealth>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if (playerHealth.getHealth() == 0)
        {
            button.SetActive(true);
        }
	}
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        UIManager.score = 0;
        MultiplierEffect.enemyDied = false;
    }
}
