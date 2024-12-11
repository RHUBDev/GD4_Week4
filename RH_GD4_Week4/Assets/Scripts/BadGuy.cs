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

    // Start is called before the first frame update
    void Start()
    {
        //Play idle animation
        animator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            //count down time till next barrel
            //if (barreltimer < barreltime)
            //{
                barreltimer += Time.deltaTime;
            //}
            //else
            //{
            if (barreltimer > barreltime && !handbarrel)
            {
                Debug.Log("Time = " + barreltimer);
                handbarrel = Instantiate(barrel, hand.transform.position, hand.transform.rotation, hand);
                animator.Play("GrenadeThrow");
                barreltimer = barreltime; 
                donethrow = false;
            }
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
                //barrelrig.constraints = RigidbodyConstraints.FreezeRotation;
                barrelrig.constraints = RigidbodyConstraints.FreezePositionZ;
                barrelrig.constraints = RigidbodyConstraints.FreezePositionX;
            }
            //}
        }
    }

    public void DoDie()
    {
        //Kill BadGuy
        alive = false;
        animator.Play("Death_01");
        barreltimer = 0;
    }
}
