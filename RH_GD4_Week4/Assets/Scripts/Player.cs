using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce = 200f;
    public Rigidbody rig;
    private bool grounded = true;
    public float gravityMultiplier = 5f;
    public GameObject splosion;
    public bool playing = true;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        animator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            //Jump if grounded
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump();
            }
        }
        //Restart Level
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    void Jump()
    {
        //Jump function
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        grounded = false;
        animator.Play("Running_Jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playing)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                //if hit ground, set grounded to true
                animator.Play("Run");
                grounded = true;
            }
            else if (collision.collider.transform.CompareTag("Barrel"))
            {
                Debug.Log("hit barrel");
                //if barrel, make explosion, and kill player
                Instantiate(splosion, collision.collider.transform.position, Quaternion.identity);
                Destroy(collision.collider.gameObject);
                playing = false;
                animator.Play("Death_01");
            }
            else if (collision.transform.CompareTag("Obstacle"))
            {
                //if any other obstacle, fall over and end game
                DoDie();
            }
            else if (collision.collider.transform.CompareTag("BadGuy"))
            {
                if (collision.contacts[0].normal.y > 0)
                {
                    //if hit top part of badguy, kill him and set grounded
                    BadGuy badguy = collision.collider.transform.GetComponent<BadGuy>();
                    badguy.DoDie();
                    Jump();
                    //grounded = true;
                    //animator.Play("Run");
                }
                else
                {
                    //if hit badguy's side or lower, he kills you
                    DoDie();
                }
            }
            else if(collision.transform.CompareTag("Truck"))
            {
                if (collision.contacts[0].normal.y > 0)
                {
                    //if landed on truck, set grounded to true
                    animator.Play("Run");
                    grounded = true;
                }
                else
                {
                    //if ran into back of truck, die
                    DoDie();
                }
            }
        }
    }

    public void DoDie()
    {
        //Kill player
        playing = false;
        animator.Play("Death_01");
    }

    void Restart()
    {
        //Restart game
        Physics.gravity /= gravityMultiplier;
        SceneManager.LoadScene("MyLevel1");
    }
}
