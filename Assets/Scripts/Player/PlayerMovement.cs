using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;     
    Rigidbody2D playerRigidBody;    
    
    // Use this for initialization
    void Awake ()
    {        
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Debug.Log(playerHealth);
        if (gameObject.GetComponentInChildren<PlayerHealth>().getHealth() > 0)
        {
            Vector2 acc = Input.acceleration;
            playerRigidBody.velocity = acc * speed;

            float angle = Mathf.Atan2(acc.y, acc.x) * Mathf.Rad2Deg - 90;
            //rotate in the z axis with given angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } 
    }

}
