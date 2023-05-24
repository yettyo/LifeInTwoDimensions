using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed = 1f;
    [SerializeField] float reach = 1f;
    float distance;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        FlipEnemyFacing();
        if(distance < reach) {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);    
        }
    }
    
    void FlipEnemyFacing() {
        //flip when gameobject is left or right of the player
        if(transform.position.x > player.transform.position.x) {
            //return to what it was before
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
        else {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
        }
        
    }
}
