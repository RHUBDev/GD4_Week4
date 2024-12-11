using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //public GameObject player;
    private float stopDistance = 0.3f;
    private float scaletimer = 0f;
    private float scaletime = 1f;
    private float rotatespeed = 180f;
    private bool triggered = false;
    public Transform followobject;
    private float coinheight = 5.32f;
    private float coinMoveSpeed = 10f;
    private float hitCoinBackMoveSpeed = 2f;

    private void Update()
    {
        if (followobject)
        {
            //if following a barrel... follow it
            transform.position = followobject.position + Vector3.up * coinheight;
            if (triggered)
            {
                //if coin got triggered, stop following barrel, so that coin movements are consistent
                followobject = null;
            }
        }
        else if (triggered)
        {
            //if moving to player, also move in the opposite direction, as it seems to cause a cool curved trajectory effect
            transform.Translate(Vector3.left * coinMoveSpeed * Time.deltaTime, Space.World);
        }
        //Rotate the coin
        transform.Rotate(0, 0, rotatespeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player hits coin, trigger coin's movement to player
        if (other.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            Player player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                StartCoroutine(MoveCoin(player));
            }
        }
    }

    IEnumerator MoveCoin(Player player)
    {
        //Modified from the Truck's 'CrushTruck' function Lerp
        Vector3 newscale = transform.localScale / 3f;
        Vector3 originalscale = transform.localScale;
        Vector3 originalposition = transform.position;
        while (((player.transform.position + Vector3.up * 1.5f) - transform.position).magnitude > stopDistance) 
        {
            //Scale coin and move towards Player
            transform.localScale = Vector3.Lerp(transform.localScale, newscale, scaletimer / scaletime);
            transform.position = Vector3.Lerp(transform.position, (player.transform.position + Vector3.up * 1.5f), scaletimer / scaletime);
            scaletimer += Time.deltaTime;
            yield return null;
        }
        
        //Add coin to player and destroy coin
        player.AddCoin();
        Destroy(gameObject);
        yield return null;
    }

    public void TriggerCoin()
    {
        //trigger the coin movement towards the player if just been spawned from the bad guy or truck
        triggered = true;
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        StartCoroutine(MoveCoin(player));
    }
}
