using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float jumpForce = 200f;
    public Rigidbody rig;
    private bool grounded = true;
    public float gravityMultiplier = 5f;
    public GameObject splosion;
    public bool playing = false;
    public Animator animator;
    private int coins = 0;
    public TMP_Text scoretext;
    public TMP_Text endtext;
    public ParticleSystem dirt;
    public AudioSource crash;
    public AudioSource jump;
    public AudioSource coinsound;
    private bool started = false;
    private float walkspeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Increase the game's gravity
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            if (transform.position.x < 2f)
            {
                transform.Translate(Vector3.right * walkspeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.position = new Vector3(2, 0, 0);
                animator.SetFloat("Speed_f", 1f);
                started = true;
                playing = true;
                dirt.Play();
            }
        }
    }

    void Jump()
    {
        dirt.Stop();
        jump.Play();
        //Jump function
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        grounded = false;
        animator.SetTrigger("Jump_trig");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playing)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                dirt.Play();
                //if hit ground, set grounded to true
                grounded = true;
            }
            else if (collision.collider.transform.CompareTag("Barrel"))
            {
                //if barrel, make explosion, and kill player
                Instantiate(splosion, collision.collider.transform.position, Quaternion.identity);
                Destroy(collision.collider.gameObject);
                DoDie();
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
                    //if hit top part of badguy, kill him and auto-jump
                    BadGuy badguy = collision.collider.transform.GetComponent<BadGuy>();
                    badguy.DoDie();
                    Jump();
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
                    dirt.Play();
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
        animator.SetBool("Death_b", true);
        crash.Play();
        dirt.Stop();
        ShowScore();
    }

    void Restart()
    {
        //Restart game
        Physics.gravity /= gravityMultiplier;
        SceneManager.LoadScene("MyLevel1");
    }

    public void DoCoinSound()
    {
        coinsound.Play();
    }

    public void AddCoin()
    {
        //Collected coin
        coins += 1;
        scoretext.text = "Coins: " + coins;
    }

    private void ShowScore()
    {
        int highscore = 0;

        //Get previous highscore
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }
        //Show UI for win/draw/loss
        if(coins > highscore)
        {
            endtext.text = "Congrats!\nBeat high score by " + (coins - highscore).ToString("n0") + "!";
            PlayerPrefs.SetInt("HighScore", coins);
        }
        else if(coins == highscore)
        {
            endtext.text = "Unlucky!\nEqualled high score";
        }
        else
        {
            endtext.text = "Unlucky!\nLost high score by " + (highscore - coins).ToString("n0");
        }
    }
}
