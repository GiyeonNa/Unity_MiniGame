using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad_bg : MonoBehaviour
{
    private MeshRenderer render;

    private float offset;
    //public float speed;

    Controller speed;

    // Start is called before the first frame update
    void Start()
    {
        
        render = GetComponent<MeshRenderer>();
        speed = GameObject.Find("player").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * (speed.diry * 0.03f);

        render.material.mainTextureOffset = new Vector2(0,offset);
    }
}
