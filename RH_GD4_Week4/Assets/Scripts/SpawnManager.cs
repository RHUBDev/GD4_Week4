using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float interval = 2f;
    public GameObject[] obstacles;
    private Vector3 spawnloc = new Vector3(20, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", interval, interval);
    }

    private void SpawnObstacle()
    {
        int obs = Random.Range(0, obstacles.Length);
        Quaternion rot = Quaternion.identity;
        Vector3 spawnloc2 = spawnloc;
        if (obs == 0)
        {
            rot = Quaternion.Euler(90, 0, 0);
            spawnloc2 += new Vector3(0, 0.467f, -0.66f);
        }
        Instantiate(obstacles[obs], spawnloc2, rot);
    }
}
