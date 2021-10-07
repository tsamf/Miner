using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    
    public float distanceFromPlayer = 5;
    public float distanceFromPlayerFall = 3;
    public float fallSpeed = 5;

    private Animator animator;
    private bool falling = false;
    private Rigidbody2D rigidbody2d;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        animator = gameObject.GetComponent<Animator>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = float.MaxValue;

        if (player != null)
        {
            distance = Mathf.Abs(transform.position.x - player.transform.position.x); ;
        }
       

        if (distance < distanceFromPlayer && !falling)
        {
            animator.SetBool("wiggle", true);
        }
        else
        {
            animator.SetBool("wiggle", false);
        }

        if(distance < distanceFromPlayerFall)
        {
            falling = true;
            animator.SetBool("wiggle", false);
            rigidbody2d.gravityScale = 1.0f;
        }
    }
}
