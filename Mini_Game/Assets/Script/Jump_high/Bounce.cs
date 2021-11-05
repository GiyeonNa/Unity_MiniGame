﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;

public class Bounce : MonoBehaviour
{
    AudioSource Audio;
    public AudioClip Jump;
    


    void Playsound(string action)
    {
        if(action == "Jump")
        {
            Audio.clip = Jump;
            Audio.Play();
        }
         
    }

        // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 650f);
            Playsound("Jump");
        }
    }
}
