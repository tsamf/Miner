using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float MovementSpeed = 10;
    public float JumpForce = 10;
    public float axefoce = 5;
    public GameObject axe;
    public AudioClip hurt;

    private bool facingLeft = false;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private bool dying = false;
    private AudioSource audioSource;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            float movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

            if (!Mathf.Approximately(0, movement))
            {
                Flip(movement);
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            if (Input.GetButton("Jump") && grounded)
            {
                grounded = false;
                rigidbody2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                animator.SetBool("Jumping", true);
                audioSource.Play();
            }
            else
            {
                animator.SetBool("Jumping", false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 axePosition = new Vector2(0, 0);

                if (facingLeft)
                {
                    axePosition = (Vector2)transform.position - new Vector2(1, 0);
                }
                else
                {
                    axePosition = (Vector2)transform.position - new Vector2(-1, 0);
                }

                GameObject newAxe = Instantiate(axe, axePosition, Quaternion.identity);

                if (facingLeft)
                {
                    newAxe.GetComponent<Rigidbody2D>().AddForce(new Vector2(-axefoce, axefoce), ForceMode2D.Impulse);
                }
                else
                {
                    newAxe.GetComponent<Rigidbody2D>().AddForce(new Vector2(axefoce, axefoce), ForceMode2D.Impulse);
                }

            }
        }
    }

    private void Flip(float movement)
    {
        if (movement > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingLeft = false;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            facingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            animator.Play("Death");
            dying = true;
            audioSource.clip = hurt;
            audioSource.Play();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            animator.Play("Death");
            dying = true;
            audioSource.clip = hurt;
            audioSource.Play();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (collision.gameObject.tag=="ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    void OnDestroy()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
