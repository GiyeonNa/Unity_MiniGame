using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GamePlayController : MonoBehaviour
{

    int ClickCount = 0;
    public static GamePlayController instance;

    public BoxSpawner box_Spawner;

    [HideInInspector]
    public BoxScript currentBox;

    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private GameObject GameOver_Msg;
    [SerializeField]
    private GameObject PauseButton;
    [SerializeField]
    private GameObject Restart;

    public CameraFollow cameraScript;
    private int moveCount;

    AudioSource AudioSource;

    /* AudioSource AudioSource;
     public AudioClip stacker;*/

    /*public void PlaySound(string action)
    {
        switch (action)
        {
            case "stacker":
                AudioSource.clip = stacker;
                AudioSource.Play();
                break;
        }

    }*/

    void DoubleClick()
    {
        ClickCount = 0;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        
        AudioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update

    public bool stopTrigger = true;
    
    
    void Start()
    {
        //box_Spawner.SpawnBox();
        
    }
    [SerializeField]
    private int score;
    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject PauseMenu;
    public void GameStart()
    {
        stopTrigger = true;
        box_Spawner.SpawnBox();

        Panel.SetActive(false);

        GameOver_Msg.SetActive(false);

        PauseButton.SetActive(true);
        Time.timeScale = 1f;

    }
    public void restart()
    {
        SceneManager.LoadScene("Stacker_Game");

        
    }
    /* public AudioSource Stack;

     public AudioClip audioClip;
    */

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            SceneManager.LoadScene("Select");

        }
        DetectInput();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {

            }

            else if (Input.GetKey(KeyCode.Escape))
            {
                
                
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
               
            }

            else if (Input.GetKey(KeyCode.Menu))
            {

            }
        }
    }

    public void Score()
    {
        score++;
        scoreTxt.text = "Score :" + score;
        
    }
    void DetectInput() // 터치시 박스 낙하
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }
    public void SpawnNewBox() // 새로운 상자 스폰
    {
        Invoke("NewBox", 0.5f);
    }
    void NewBox()
    {
        box_Spawner.SpawnBox();
    }
    public void MoveCamera() // 카메라 움직이기
    {
        moveCount++;
            if(moveCount == 3)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 2.5f;
        }
    }
    public void RestartGame() //다시시작
    {
        stopTrigger = false;

       // Panel.SetActive(true);

        GameOver_Msg.SetActive(true);
        PauseButton.SetActive(false);
        bestScore.text = "내가 쌓은 상자 수 : " + score;
        //StopCoroutine(DetectInput());

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}
