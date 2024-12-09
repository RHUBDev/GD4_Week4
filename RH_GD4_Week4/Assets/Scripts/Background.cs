using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float scrollspeed = 1f;
    public Material mat;
    private float offset = 0f;

    private void Start()
    {
        scrollspeed = 1 / 11.28f;
    }

    // Update is called once per frame
    void Update()
    {
        offset += scrollspeed * Time.deltaTime;
        mat.SetTextureOffset("_BaseMap", new Vector2(offset, 0f));
    }
}
