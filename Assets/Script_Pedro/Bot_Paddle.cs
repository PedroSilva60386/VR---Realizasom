using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Paddle : MonoBehaviour
{
    [SerializeField] private GameObject puck;

    private Rigidbody rb;

    private Vector3 playerMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()   
    {
        if(puck.transform.position.x > transform.position.x + 0.25f)
            playerMove= new Vector3(0.5f, 0,0);
        if(puck.transform.position.x < transform.position.x - 0.25f)
            playerMove= new Vector3(-0.5f, 0,0);
    }
    

    private void FixedUpdate()
    {
        rb.velocity = playerMove * 2f;
    }
}
