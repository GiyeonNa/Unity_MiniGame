using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float moveInput;
    private float speed = 20f;

    private bool isStarted = false;
    public Text scoreText;
    private float topScore = 0.0f;
    public Text starttext;

    public GameObject deathMenu;

    Animator anim;

    public GameOver Over;

    //기울기 가속도
    float dirx;

    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector3.zero;
        anim = GetComponent<Animator>();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        /*dirx = Input.acceleration.x * speed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
        */

        if (Input.GetMouseButtonDown(0) && isStarted == false)
        {
            isStarted = true;
            starttext.gameObject.SetActive(false);
            rb2d.gravityScale = 5f;
        }

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
           
            rb2d.velocity = new Vector2(dirx, rb2d.velocity.y);
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