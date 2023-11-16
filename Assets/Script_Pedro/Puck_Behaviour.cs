using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Puck_Behaviour : MonoBehaviour
{
    [SerializeField]
    private Transform leftWall;
    [SerializeField]
    private Transform rightWall;
    [SerializeField]
    private Transform topWall;
    [SerializeField]
    private Transform bottomtWall;
    
    [SerializeField]
    private float forceMultiplier = 5f;
    [SerializeField]
    private string inputAction = "Fire1"; // Change to the desired action
    private Rigidbody rb;
    [SerializeField]
    private GameObject paddle;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown(inputAction))
        {
            Vector3 v = new Vector3(Random.value, 0, Random.value) * forceMultiplier;
            rb.AddForce(v);
        }
        colisionWalls();
        colisionPaddle();
        
        Vector3 vv = rb.velocity;
        rb.velocity = vv * 0.999f;
    }

    void colisionPaddle()
    {
        Vector3 puckPosition = transform.position;
        //if ((puckPosition.x + transform.localScale.x < paddle.transform.position.x + paddle.transform.localScale.x))
        //{
            //Vector3 v = rb.velocity;
            //rb.velocity = new Vector3(-v.x, v.y, -v.z);
        //}

        if ((puckPosition.z + transform.localScale.z < paddle.transform.position.z + paddle.transform.localScale.z))
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }
        
        
    }

    void colisionWalls()
    {
        Vector3 puckPosition = transform.position;
        if (puckPosition.x + transform.localScale.x > rightWall.position.x)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(-v.x, v.y, v.z);
        }
        
        if (puckPosition.x - transform.localScale.x < leftWall.position.x)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(-v.x, v.y, v.z);
        }
        
        
        if (puckPosition.z - transform.localScale.z < bottomtWall.position.z)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }
        
        
        if (puckPosition.z + transform.localScale.z> topWall.position.z)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }
    }


}