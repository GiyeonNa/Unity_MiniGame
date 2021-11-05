using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectControl : MonoBehaviour
{

    GameObject obj;
    


    void Start()
    {
        //obj = GameObject.Find("MainMusic");
        
    }

    // Start is called before the first frame update
    public void Match_2()
    {
        // obj.GetComponent<MusicControl>().BgmStop();
        SceneManager.LoadScene("Match_2");
    }

    public void Stacker()
    {

        SceneManager.LoadScene("Stacker_Game");
    }

    public void jump()
    {

        SceneManager.LoadScene("Jump_high");
    }

    public void Poop()
    {

        SceneManager.LoadScene("Poop_Game");
    }
}

