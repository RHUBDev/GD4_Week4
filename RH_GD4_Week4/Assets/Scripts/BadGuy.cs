using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuy : MonoBehaviour
{
    public Animator animator;
    private float barreltime = 0.5f;
    private float barreltimer = 0f;
    private bool alive = true;
    public Transform hand;
    public GameObject barrel;
    private float barrelthrowtime = 2.0f;
    private GameObject handbarrel;
    private bool donethrow = false;
    private float barrelresettime = -0.812844f;
    public GameObject coinprefab;
    private int coinstospawn = 3;
    private float cointime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            barreltimer += Time.deltaTime;
            //grab barrel and start throw animation
            if (barreltimer > barreltime && !handbarrel)
            {
                handbarrel = Instantiate(barrel, hand.transform.position, hand.transform.rotation, hand);
                animator.Play("GrenadeThrow");
                barreltimer = barreltime; 
                donethrow = false;
            }
            //release barrel and set the barrel's setings
            if (barreltimer > barrelthrowtime && !donethrow)
            {
                donethrow = true;
                barreltimer = barrelresettime;
                handbarrel.transform.position = transform.position + Vector3.left * 1.4f + Vector3.up * 2f;
                handbarrel.transform.SetParent(null);
                handbarrel.GetComponent<Collider>().isTrigger = false;
                handbarrel.GetComponent<Obstacle>().enabled = true;
                Barrel barrel1 = handbarrel.GetComponent<Barrel>();
                barrel1.thrown = true;
                Rigidbody barrelrig = handbarrel.AddComponent<Rigidbody>();
                barrelrig.isKinematic = false;
                barrelrig.constraints = RigidbodyConstraints.FreezeRotationX;
                barrelrig.constraints = RigidbodyConstraints.FreezePositionZ;
                barrelrig.constraints = RigidbodyConstraints.FreezePositionX;
                barrelrig.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            //}
        }
    }

    public void DoDie()
    {
        //Kill BadGuy
        alive = false;
        animator.SetBool("Death_b", true);
        barreltimer = 0;
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < coinstospawn; i++)
        {
            Debug.Log("BadGuy Coin");
            //Spawn the correct number of coins
            GameObject coin = Instantiate(coinprefab, transform.position, Quaternion.Euler(90, 180, 0));
            coin.GetComponent<Coin>().TriggerCoin();
            yield return new WaitForSeconds(cointime);
        }
        yield return null;
    }
}
