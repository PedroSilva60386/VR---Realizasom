using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Puck_Behaviour : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;
    public Transform topWall;
    public Transform bottomtWall;
    
    public float forceMultiplier = 5f;
    public string inputAction = "Fire1"; // Change to the desired action
    private Rigidbody rb;

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

        Vector3 pp = transform.position;
        if (pp.x > rightWall.position.x)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(-v.x, v.y, v.z);
        }
        
        if (pp.x < leftWall.position.x)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(-v.x, v.y, v.z);
        }
        
        
        if (pp.z < bottomtWall.position.z)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }
        
        
        if (pp.z  > topWall.position.z)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }

        Vector3 vv = rb.velocity;

        rb.velocity = vv * 0.999f;
    

    }


}