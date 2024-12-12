using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float moveSpeed = 10;
    private float deadMoveSpeed = 2;
    private float rotateSpeed = 140;

    private float rotateSpeed2 = 280;
    private bool isbarrel = false;
    private Player player;
    private Barrel barrel;

    private void Start()
    {
        //on start, find player, and speed up (roll) if barrel
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (gameObject.CompareTag("Barrel"))
        {
            isbarrel = true;
            moveSpeed = 12f;
        }
        barrel = GetComponent<Barrel>();
    }

    private void OnEnable()
    {
        //on enable, find player, and speed up (roll) if barrel
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (gameObject.CompareTag("Barrel"))
        {
            isbarrel = true;
            moveSpeed = 12f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (player.playing)
            {
                //moveSpeed obstacle forwards
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);

                if (isbarrel && !barrel)
                {
                    //if barrel, rotate too
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                }
                else if(isbarrel &&barrel)
                {
                    transform.Rotate(Vector3.up * rotateSpeed2 * Time.deltaTime);
                }
            }
            else if(isbarrel && !barrel)
            {
                //if gameover, keep barrel rotating, and moving slower
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                moveSpeed = deadMoveSpeed;
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            }
            else if (isbarrel && barrel)
            {
                transform.Rotate(Vector3.up * rotateSpeed2 * Time.deltaTime);
            }
        }
        if (transform.position.x < -5)
        {
            //destroy obstacle if out of screen bounds
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if barrel hit another obstacle, stop moving and rotating it 
        if (collision.transform.CompareTag("Obstacle"))
        {
            deadMoveSpeed = 0;
            rotateSpeed = 0;
        }
    }
}
