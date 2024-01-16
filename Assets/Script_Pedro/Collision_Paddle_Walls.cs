using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Paddle_Walls : MonoBehaviour
{
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private Transform leftBottomSideWall;
    [SerializeField] private Transform rightBottomSideWall;
    [SerializeField] private AudioSource collisionAudio;
    // Start is called before the first frame update
    void Start()
    {
        rightWall.gameObject.name = "RightWall";
        leftWall.gameObject.name = "LightWall";
        bottomWall.gameObject.name = "BottomWall";
        leftBottomSideWall.gameObject.name = "LeftBottomSideWall";
        rightBottomSideWall.gameObject.name = "RightBottomSideWall";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision c)
    { 
        switch (c.gameObject.name)
        {
            case "RightWall":
            case "LeftWall":
            case "BottomWall":
            case "LeftBottomSideWall":
            case "RightBottomSideWall":
                collisionAudio.Play();
                break;
            }
    }
}
