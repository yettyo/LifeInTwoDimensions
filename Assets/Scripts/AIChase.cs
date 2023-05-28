using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed = 1f;
    [SerializeField] float reach = 1f;
    float distance;
    public bool isPlayerAlive = true;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object in the scene
    }

    
    void Update()
    {   
        if (player == null) {
            return; //exit the function if the player is null
        }
        if (!isPlayerAlive)
    {
        return;
    }
        chaseWhenClose();
        FlipEnemyFacing();
    }
    
    void FlipEnemyFacing() {
        if (player == null) {
            return; //exit the function if the player is null
        }
        //flip when gameobject is left or right of the player
        if(transform.position.x > player.transform.position.x) {
            //return to what it was before
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
        else {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
        }
        
    }

    void chaseWhenClose() {
        if (player == null) {
            return; //exit the function if the player is null
        }  
        if(Vector2.Distance(transform.position, player.transform.position) < reach) {
            distance = Vector2.Distance(transform.position, player.transform.position);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);    
        }
    }
}
