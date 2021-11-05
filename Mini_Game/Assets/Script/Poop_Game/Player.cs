using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D zrigidbody;

    private Animator animator;

    private SpriteRenderer zrenderer;


	// Use this for initialization
	void Start () {
        zrigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        zrenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () { // start and End 구현 
         

        if (GameManager.Instance.stopTrigger)
        {
            animator.SetTrigger("start");
           
        }

        if (!GameManager.Instance.stopTrigger)
        {
            animator.SetTrigger("dead");
        }
        
       
    }  
     }
