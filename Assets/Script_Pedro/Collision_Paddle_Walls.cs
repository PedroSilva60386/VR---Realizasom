using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Paddle_Walls : MonoBehaviour
{
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private AudioSource collisionAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == rightWall.gameObject.name)
        {
            collisionAudio.Play();
        }
        if (c.gameObject.name == leftWall.gameObject.name)
        {
            collisionAudio.Play();
        }
        if (c.gameObject.name == bottomWall.gameObject.name)
        {
            collisionAudio.Play();
        }
    }
}
