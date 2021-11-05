using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Over : MonoBehaviour
{
    GameObject obj;
    public Text pointText;

    public void Start()
    {
        obj = GameObject.Find("MainMusic");
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointText.text = score.ToString() + " Points";
    }

    public void Restart()
    {
        //SceneManager.LoadScene("Match_2");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //obj.GetComponent<MusicControl>().BgmPlay();
        SceneManager.LoadScene("Select");
    }
}
