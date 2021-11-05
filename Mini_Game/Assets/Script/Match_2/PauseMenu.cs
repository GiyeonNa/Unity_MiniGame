using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    AudioSource AudioSource;

    public AudioClip pausebtn;
    public AudioClip resumebtn;
    public AudioClip mainmenubtn;

    GameObject obj;
    GameManager asdf;


    public void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        
        obj = GameObject.Find("MainMusic");
    }

    void Playsound(string action)
    {
        switch (action)
        {
            case "Pause":
                AudioSource.clip = pausebtn;
                AudioSource.Play();
                break;
            case "Resume":
                AudioSource.clip = resumebtn;
                AudioSource.Play();
                break;
            case "Mainmenu":
                AudioSource.clip = mainmenubtn;
                AudioSource.Play();
                break;
            
        }

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        //tiemScale 1f = realtime
        //timeScale 0.5f = 2x slower
        //timeScale 0f = stop
        Time.timeScale = 0f;
        Playsound("Pause");

        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Playsound("Resume");

        
    }

    public void Mainmenu()
    {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(id);

       // obj.GetComponent<MusicControl>().BgmPlay();
       // Playsound("Mainmenu");

        SceneManager.LoadScene("Select");
    }
}
