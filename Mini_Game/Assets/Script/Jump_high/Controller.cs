using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    int ClickCount = 0;
    private Rigidbody2D rb2d;
    private float moveInput;
    private float speed = 20f;

    private bool isStarted = false;
    public Text scoreText;
    private float topScore = 0.0f;
    

    public GameObject deathMenu;

    Animator anim;

    public GameOver Over;

    public GameObject P_button;

    //기울기 가속도
    float dirx;
    public float diry;

    public PauseMenu Pause;

    public GameObject Startmenu;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector3.zero;
        anim = GetComponent<Animator>();
        
        
    }
    void Awake()
    {
        Time.timeScale = 1f;
    }
    public void GameStart()
    {
        Startmenu.SetActive(false);
        scoreText.gameObject.SetActive(true);
        isStarted = true;
        //starttext.gameObject.SetActive(false);
        rb2d.gravityScale = 5f;
        P_button.SetActive(true);
    }

    void DoubleClick()
    {
        ClickCount = 0;
    }
    private void Update()
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

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {

            }

            else if (Input.GetKey(KeyCode.Escape))
            {
                Pause.Pause();
            }

            else if (Input.GetKey(KeyCode.Menu))
            {

            }
        }

        dirx = Input.acceleration.x * speed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
        

        /*if (Input.GetMouseButtonDown(0) && isStarted == false)
        {
            isStarted = true;
            starttext.gameObject.SetActive(false);
            rb2d.gravityScale = 5f;
        }*/

        if (isStarted == true)
        {
            if (dirx < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }

            if(rb2d.velocity.y > 0)
            {
                anim.SetBool("isJump", true);
            }
            else
            {
                anim.SetBool("isJump", false);
            }


            //y속도 상승중 && y위치가 topScore보다 높다면
            if (rb2d.velocity.y > 0 && transform.position.y > topScore)
            {
                topScore = transform.position.y;
            }

            scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
        }

        if (transform.position.y <= topScore - 40)
        {
            GameOver();
        }

       
        

    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (isStarted == true)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
        }

        /*if (isStarted == true)
        {
            if (Input.acceleration.x > this.threshold)
            {
                moveInput = 1;
                rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
            }

            if (Input.acceleration.x < this.threshold)
            {
                moveInput = -1;
                rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
            }
        }*/
        
        if(isStarted == true)
        {
            diry = rb2d.velocity.y;
            //rb2d.velocity = new Vector2(moveInput * speed, diry);
            rb2d.velocity = new Vector2(dirx, diry);
        }  
    }
    
  
    void GameOver()
    {
        deathMenu.SetActive(true); //to set active the deathmenu that i created on canvas
        scoreText.gameObject.SetActive(false);
        Time.timeScale = 0; //to freeze the game
        Over.Setup((int)topScore);
        //AudioSource.Stop();
    }
}