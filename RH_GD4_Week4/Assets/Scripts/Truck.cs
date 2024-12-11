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
    public GameObject truckbody;
    private float crushsize = 0.5f;
    private float crushtime = 0.2f;
    private float crushtimer = 0f;
    public GameObject coinprefab;
    private int coinstospawn = 5;
    private float cointime = 0.1f;
    private bool coining = false;

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
            //if player dead, move truck away from player, now that obstacles have stopped moving
            if (!isdead)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
            }
        }
       

        if ((transform.position.x < -8f || transform.position.x > 27f) && !coining)
        {
            //destroy truck if out of screen bounds, and not giving out coins 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player hits the trigger at the front of the truck, blow it up
        if (other.transform.CompareTag("Player"))
        {
            DoKillTruck();
        }
    }

    private void DoKillTruck()
    {
        if (!isdead)
        {
            //Blow up truck
            isdead = true;
            Instantiate(splosionPrefab, transform.position, transform.rotation);
            rig.AddExplosionForce(splosionForce, transform.position + splosionoffset, splosionradius);
            StartCoroutine(CrushTruck());
        }
    }

    IEnumerator CrushTruck()
    {
        //set coining bool so truck doesn't get deleted before coins are created
        coining = true;
        //I googled how to do the Lerp again
        Vector3 newscale = new Vector3(truckbody.transform.localScale.x, crushsize, truckbody.transform.localScale.z);
        while (truckbody.transform.localScale.y > crushsize)
        {
            truckbody.transform.localScale = Vector3.Lerp(truckbody.transform.localScale, newscale, crushtimer / crushtime);
            crushtimer += Time.deltaTime;
            yield return null;
        }
        truckbody.transform.localScale = newscale;
        StartCoroutine(SpawnCoins());
        yield return null;
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < coinstospawn; i++)
        {
            //Spawn the correct number of coins
            Debug.Log("Truck Coin");
            GameObject coin = Instantiate(coinprefab, transform.position, Quaternion.Euler(90, 180, 0));
            coin.GetComponent<Coin>().TriggerCoin();
            yield return new WaitForSeconds(cointime);
        }
        coining = false;
        yield return null;
    }
}
