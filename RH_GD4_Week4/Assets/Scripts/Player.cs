using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 200f;
    public Rigidbody rig;
    private bool grounded = true;
    public float gravityMultiplier = 5f;
    public GameObject splosion;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            grounded = true;
        }
        else if (collision.transform.CompareTag("Barrel"))
        {
            Instantiate(splosion, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
