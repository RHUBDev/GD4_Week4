using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float moveSpeed = 10;
    private float rotateSpeed = 140;
    private bool isbarrel = false;
    private Player player;

    private void Start()
    {
        //on start, find player, and speed up (roll) if barrel
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (gameObject.CompareTag("Barrel"))
        {
            isbarrel = true;
            moveSpeed = 12f;
        }
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

                if (isbarrel)
                {
                    //if barrel, rotate too
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                }

                if (transform.position.x < -5)
                {
                    //destroy obstacle if out of screen bounds
                    Destroy(gameObject);
                }
            }
        }
    }
}
