using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierEffect : MonoBehaviour {

    GameObject player;
    public static bool enemyDied;
    public static int tempScore;
    public static int enemyDeathCounter;

    public Text multiplier;
    public Text finalScore;
    public Text score;

    int tempScoreCopy;
    int enemyDeathCounterCopy;
    float speed;
    float timer;
    Vector2 scorePos;
    Vector2 lastHit;
    bool scoreFly;
    IEnumerator coroutine;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        multiplier.text = "";
        finalScore.transform.position = getPlayerPos();
        scorePos = score.transform.position;
        scorePos.x += 2.6f;
        scorePos.y -= .35f;
        speed = 7;
    }

    void Update()
    {
        //if (player.GetComponent<PlayerHealth>().getHealth() > 0)
        //{
        
            if (enemyDied && player.GetComponent<PlayerHealth>().getHealth() > 0)
            {
                enemyDeathCounter += 1;

                lastHit = multiplier.transform.position = getPlayerPos();                
                enemyDied = false;
                timer = 0;
                multiplier.text = enemyDeathCounter + "x";
                StartCoroutine(FadeText(1f, multiplier));
            }

            if (enemyDeathCounter >= 1)
            {
                if (Mathf.Floor(timer) < 1)
                {
                    timer += Time.deltaTime;
                }
                else if (Mathf.Floor(timer) >= 1)
                {
                    scoreFly = true;
                    //UIManager.score += tempScore * enemyDeathCounter;
                    tempScoreCopy = tempScore;
                    enemyDeathCounterCopy = enemyDeathCounter;
                    tempScore = 0;
                    enemyDeathCounter = 0;
                }
            }
        //}

        if (scoreFly)
        {            
            finalScore.text = "" + tempScoreCopy * enemyDeathCounterCopy;

            finalScore.transform.position = Vector2.MoveTowards(finalScore.transform.position, scorePos, speed * Time.deltaTime);
            //Debug.Log(score.transform.position);
        }

        if ((Vector2)finalScore.transform.position == scorePos && scoreFly)
        {
            UIManager.score += tempScoreCopy * enemyDeathCounterCopy;
            finalScore.text = "";
            finalScore.transform.position = lastHit;
            scoreFly = false;
        }
    }

    int randomSign()
    {
        float sign = Random.Range(0, 2);
        if (sign >= 1)
        {
            return 1;
        }
        else
        {
            return -1;
        }

    }

    Vector2 getPlayerPos()
    {
        float x = Random.Range(0.3f, 0.5f) * randomSign();
        float y = Random.Range(0.3f, 0.5f) * randomSign();
        Vector2 playerPos = player.transform.position;
        playerPos.x += x;
        playerPos.y += y;
        return playerPos;
    }

    IEnumerator FadeText(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
