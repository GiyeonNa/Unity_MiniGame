using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    GameObject Bat;
    BatControl2 batControl2;
    // Start is called before the first frame update
    void Start()
    {
        Bat = GameObject.Find("Player");
        batControl2 = Bat.GetComponent<BatControl2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftBtnDown()
    {
        batControl2.LeftMove = true;
    }
    public void LeftBtnUp()
    {
        batControl2.LeftMove = false;
    }

    public void RightBtnDown()
    {
        batControl2.RightMove = true;
    }
    public void RightBtnUp()
    {
        batControl2.RightMove = false;
    }

}
