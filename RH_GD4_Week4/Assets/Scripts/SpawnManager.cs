using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float intervalmin = 0.9f;
    private float intervalmax = 1.3f;
    public GameObject[] obstacles;
    private Vector3 spawnloc = new Vector3(20, 0, 0);
    public Player player;
    private int obsspawned = 0;
    private int obstowait = 7;
    public GameObject[] trucks;
    private bool spawnedtruck = false;
    private GameObject truckobj;
    private int trucksspawned = 0;
    public GameObject coinprefab;
    private float coinheight = 5.787f;

    // Start is called before the first frame update
    void Start()
    {
        //spawn obstacles
        InvokeRepeating("SpawnObstacle", 2f, Random.Range(intervalmin, intervalmax));
    }

    private void SpawnObstacle()
    {
        //if still playing, spawn random obstacle
        if (player.playing)
        {
            int obs = Random.Range(0, obstacles.Length);
            Quaternion rot = Quaternion.Euler(0, -90, 0);
            Vector3 spawnloc2 = spawnloc;
            if (obs == 0)
            {
                rot = Quaternion.Euler(90, 0, 0);
                spawnloc2 += new Vector3(0, 0.467f, -0.66f);
            }
            GameObject go = Instantiate(obstacles[obs], spawnloc2, rot);
            if (obs == 0)
            {
                GameObject coin1 = Instantiate(coinprefab, spawnloc2 + Vector3.up * coinheight, Quaternion.Euler(90, 180, 0));
                coin1.GetComponent<Coin>().followobject = go.transform;
            }
            obsspawned += 1;
        }
    }

    private void Update()
    {
        if (player.playing)
        {
            if (!spawnedtruck)
            {
                if (obsspawned >= obstowait)
                {
                    //after 7 obstacles, spawn truck, and cancel invoke
                    spawnedtruck = true;
                    CancelInvoke();
                    Quaternion rot = Quaternion.Euler(0, 90, 0);
                    Vector3 spawnloc2 = new Vector3(26, 0, 0);
                    truckobj = Instantiate(trucks[trucksspawned], spawnloc2, rot);
                    //set number of the next truck
                    trucksspawned += 1;
                    if (trucksspawned >= trucks.Length)
                    {
                        trucksspawned = 0;
                    }
                }
            }
            else if (!truckobj)
            {
                //once truck destroyed, start spawning the next wave of obstacles
                obsspawned = 0;
                spawnedtruck = false;
                InvokeRepeating("SpawnObstacle", 2f, Random.Range(intervalmin, intervalmax));
            }
        }
    }
}
