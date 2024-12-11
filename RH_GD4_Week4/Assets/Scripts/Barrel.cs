using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public bool thrown = false;
    private float moveSpeed = 5f;
    //Late Update to override the rotation of it's parent (bad guy's hand)
    void LateUpdate()
    {
        if (!thrown)
        {
            //if not yet thrown, set barrel rotation
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            //if thrown, move the barreltowards player
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if barrel hit another obstacle, stop moving and rotating it 
        if (collision.transform.CompareTag("Obstacle"))
        {
            moveSpeed = 0;
        }
    }
}
