using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControl2 : MonoBehaviour
{
  //  Animator animator;
    public bool LeftMove = false;
    public bool RightMove = false;
    Vector3 moveVelocity = Vector3.zero;
    float moveSpeed = 60;

    // Start is called before the first frame update
    void Start()
    {
       // animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftMove) //터치 이동 구현
        {
          //  animator.SetBool("Direction", false);
            moveVelocity = new Vector3(-0.10f, 0, 0);
            transform.position += moveVelocity * moveSpeed * Time.deltaTime;
        }
        if (RightMove)
        {
           // animator.SetBool("Direction", true);
            moveVelocity = new Vector3(+0.10f, 0, 0);
            transform.position += moveVelocity * moveSpeed * Time.deltaTime;
        }
        ScreenChk();
    }

    private void ScreenChk() // 옆으로 안나가게 하는 함수 
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
