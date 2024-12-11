using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float deadMoveSpeed = 10f;
    public Rigidbody rig;
    private float splosionForce = 200f;
    public GameObject splosionPrefab;
    private Player player;
    private Vector3 splosionoffset = new Vector3(-3, -1, 0);
    private float splosionradius = 3;
    private bool isdead = false;
    private void Start()
    {
        //on start, find player
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move truck
        if (player.playing)
        {
            if (!isdead)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector3.left * deadMoveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            if (!isdead)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            }
        }
       

        if (transform.position.x < -8f || transform.position.x > 27f)
        {
            //destroy truck if out of screen bounds
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            DoKillTruck();
        }
    }

    private void DoKillTruck()
    {
        isdead = true;
        Instantiate(splosionPrefab, transform.position, transform.rotation);
        rig.AddExplosionForce(splosionForce, transform.position + splosionoffset, splosionradius);
        //rig.AddRelativeTorque(splosionForce, 0, 0,ForceMode.Impulse);
    }
}
