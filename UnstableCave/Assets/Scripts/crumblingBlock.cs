using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumblingBlock : MonoBehaviour
{

    public AnimationClip crumble;

    private Animator animator;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.Play("crumbling");
            audioSource.Play();
            Destroy(gameObject, crumble.length);
        }
    }
}
