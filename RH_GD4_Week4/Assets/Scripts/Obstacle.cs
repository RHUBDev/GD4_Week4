using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float moveSpeed = 10;
    private float rotateSpeed = 140;
    private bool isbarrel = false;
    private void Start()
    {
        if (gameObject.CompareTag("Barrel"))
        {
            isbarrel = true;
            moveSpeed = 12f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);

        if (isbarrel)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

        if(transform.position.x < -5)
        {
            Destroy(gameObject);
        }
    }
}
