using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public bool thrown = false;
    private float moveSpeed = 5f;
    // Update is called once per frame
    void LateUpdate()
    {
        if (!thrown)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
