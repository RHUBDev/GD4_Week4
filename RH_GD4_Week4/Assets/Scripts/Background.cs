using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float scrollspeed = 1f;
    public Material mat;
    private float offset = 0f;
    public Player player;

    private void Start()
    {
        mat.SetTextureOffset("_BaseMap", new Vector2(0f, 0f));
        //scrollspeed set by 1 / (width of quad / speed of obstacles(10))
        scrollspeed = 1 / 11.28f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playing)
        {
            //scroll the texture
            offset += scrollspeed * Time.deltaTime;
            mat.SetTextureOffset("_BaseMap", new Vector2(offset, 0f));
        }
    }
}
