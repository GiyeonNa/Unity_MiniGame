using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    int ClickCount = 0;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
            
        }
    }
    private void Awake()
    {
        
        AudioSource = GetComponent<AudioSource>();

    }
    [SerializeField]
    private GameObject poop;

    private int score;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Transform objbox;
  

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private GameObject GameOver_Msg;
    [SerializeField]
    public GameObject R_buttons;

    [SerializeField]
    public GameObject L_buttons;

    [SerializeField]
    private GameObject PauseButton;

    [SerializeField]
    private  GameObject PauseMenu;
    //사운드
    AudioSource AudioSource;
    public AudioClip dath;
    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;

    }
	
	// Update is called once per frame
	void Update () {

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
 /*#if !UNITY_EDITOR
        //System.Diagnostics.Process.GetCurrentProcess().kill();
#endif*/
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {

            }

            else if (Input.GetKey(KeyCode.Escape))
            {
                
                PauseMenu.SetActive(true);
                R_buttons.SetActive(false);
                L_buttons.SetActive(false);
                Time.timeScale = 0f;
            }

            else if (Input.GetKey(KeyCode.Menu))
            {

            }
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "dath":
                AudioSource.clip = dath;
                AudioSource.Play();
                break;
        }

    }
    public bool stopTrigger = true;
    public void GameOver()
    {
          stopTrigger = false;

          StopCoroutine(CreatepoopRoutine());

        bestScore.text = "내가 피한 벽돌 수 : " + score;

         // if(score >= PlayerPrefs.GetInt("BestScore", 0))
          //PlayerPrefs.SetInt("BestScore",score);

          //bestScore.text = PlayerPrefs.GetInt("BestScore",0).ToString();

            GameOver_Msg.SetActive(true);
            //Panel.SetActive(true);
          R_buttons.SetActive(false);
          L_buttons.SetActive(false);
        PauseButton.SetActive(false);
        scoreTxt.gameObject.SetActive(false);

        PlaySound("dath");
        
        
        

        


        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        score = 0;
        scoreTxt.text = "Score : " + score;
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        
        Panel.SetActive(false);
        GameOver_Msg.SetActive(false);
       
        R_buttons.SetActive(true);
        L_buttons.SetActive(true);
        PauseButton.SetActive(true);

        scoreTxt.gameObject.SetActive(true);

    }

    public void Restart()
    {   
        SceneManager.LoadScene("Poop_Game");
        
    }
    public void Score()
    {
        if(stopTrigger)
        score++;
        scoreTxt.text = "Score : " + score;
    }

    IEnumerator CreatepoopRoutine()
    {
        while (stopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(0.3f);
        }        
    }

    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 5.0f)); //벽돌 설정
        //pos.z = 0.0f;
        GameObject obj = Instantiate(poop,pos,Quaternion.identity);
        obj.transform.parent = objbox.transform;
    }
    void DoubleClick()
    {
        ClickCount = 0;
    }

}
