using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;
    public TMP_Text scoretext;
    public TMP_Text endtext;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y < 14.12f)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (transform.position.y > 14.12f && !gameOver)
        {
            transform.position = new Vector3(transform.position.x, 14.12f, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            playerRb.useGravity = false;
            endtext.text = "Game Over!\nYou Scored: $" + score + "!";
        }
        else if (other.transform.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            playerAudio.PlayOneShot(boingSound, 1.0f);
        }
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            score += 1;
            scoretext.text = "Score: $" + score.ToString("n0");
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
