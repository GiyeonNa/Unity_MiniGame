using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_manager : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    //public Controller speed;
    Controller asdf;
    

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        asdf = GameObject.Find("player").GetComponent<Controller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = asdf.diry;
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * (speed * 0.3f) * Time.deltaTime;
        transform.position = curPos + nextPos;
        
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            Vector3 backspritePos = sprites[startIndex].localPosition;
            Vector3 frontspritePos = sprites[endIndex].localPosition;

            sprites[endIndex].transform.localPosition = backspritePos + Vector3.up * 20;

            int startIndexsave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexsave - 1 == -1) ? sprites.Length - 1 : startIndexsave - 1;
        }
    }
}
