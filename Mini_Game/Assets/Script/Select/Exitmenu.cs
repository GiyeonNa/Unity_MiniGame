using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitmenu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        menu.SetActive(false);
    }
}
