using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -2f, max_X = 2f; //양옆으로 박스가 움직이는 메소드

    private bool canMove;
    private float move_Speed = 4f;

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    AudioSource AudioSource;
    public AudioClip stacker;
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "stacker":
                AudioSource.clip = stacker;
                AudioSource.Play();
                break;
        }

    }

    // private GamePlayController sound;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f; // 박스 안떨어지고 위에 고정
    }
    // Start is called before the first frame update
    void Start()
    {
       
        canMove = true;

        if(Random.Range(0, 2) > 0)
        {
            move_Speed *= -1f;
        }
        GamePlayController.instance.currentBox = this;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
        ScreenChk();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += move_Speed * Time.deltaTime;
           

            if (temp.x > max_X)
            {
                move_Speed *= -1f;
            } else if (temp.x < min_X)
            {
                move_Speed *= -1f;
            }
            transform.position = temp;
        }
    }
        
        public void DropBox() {
            canMove = false;
            myBody.gravityScale = Random.Range(2, 4);
        }

    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GamePlayController.instance.RestartGame();
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platfom")
        {
            Invoke("Landed", 0.5f);
            ignoreCollision = true;
            GamePlayController.instance.Score();
            PlaySound("stacker");
        }
        if (target.gameObject.tag == "Box")
        {
            Invoke("Landed", 0.5f);
            ignoreCollision = true;
            GamePlayController.instance.Score();
            PlaySound("stacker");
        }
        
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;
        if(target.tag == "GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 1f);
        }
    }
   
    private void ScreenChk() // 옆으로 안나가게 하는 함수 
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
