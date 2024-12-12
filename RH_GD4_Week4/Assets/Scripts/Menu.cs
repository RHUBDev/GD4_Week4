using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator animator;
    private float scrollspeed = 1f;
    public Material mat;
    private float offset = 0f;
    private bool stopped = false;
    private float moveSpeed = 10;
    // Start is called before the first frame update
    private void Start()
    {
        mat.SetTextureOffset("_BaseMap", new Vector2(0f, 0f));
        //scrollspeed set by 1 / (width of quad / speed of obstacles(10))
        scrollspeed = 1 / 11.28f;
        animator.SetFloat("Speed_f", 1f);
    }

    void Update()
    {
        if(!stopped)
        {
            //scroll the texture
            offset += scrollspeed * Time.deltaTime;
            mat.SetTextureOffset("_BaseMap", new Vector2(offset, 0f));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                stopped = true;
                StartCoroutine(StartGame());
            }
        }
        else
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("MyLevel1");
    }
}
