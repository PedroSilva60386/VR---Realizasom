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
    [SerializeField]
    private GameObject paddle;
    
    private Rigidbody rb;
    private float puckRadius;
    private float paddleRadius;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        puckRadius = transform.localScale.x;
        paddleRadius = paddle.transform.localScale.x;
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown(inputAction))
        {
            Vector3 v = new Vector3(Random.value, 0, Random.value) * forceMultiplier;
            rb.AddForce(v);
        }
        ColisionWalls();
        ColisionPaddle();
        
        Vector3 vv = rb.velocity;
        rb.velocity = vv * 0.999f;
    }

    void ColisionPaddle()
    {
        Vector3 puckPosition = transform.position;
        if (paddle.transform.position.x <= 0)
        {
            if ((puckPosition.x + puckRadius <= paddle.transform.position.x + paddleRadius) && (puckPosition.z + puckRadius <= paddle.transform.position.z + paddleRadius))
            {
                Vector3 v = rb.velocity;
                rb.velocity = new Vector3(-v.x, v.y, -v.z);
            }
        }

        else if (paddle.transform.position.x > 0)
        {
            if ((puckPosition.x + puckRadius <= paddle.transform.position.x + paddleRadius) && (puckPosition.z + puckRadius > paddle.transform.position.z + paddleRadius))
            {
                Vector3 v = rb.velocity;
                rb.velocity = new Vector3(-v.x, v.y, -v.z);
            }
        }
    }

    void ColisionWalls()
    {
        Vector3 puckPosition = transform.position;
        if (puckPosition.x + puckRadius > rightWall.position.x)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(-v.x, v.y, v.z);
        }
        
        if (puckPosition.x - puckRadius < leftWall.position.x)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(-v.x, v.y, v.z);
        }
        
        
        if (puckPosition.z - puckRadius < bottomtWall.position.z)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }
        
        
        if (puckPosition.z + puckRadius > topWall.position.z)
        {
            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, v.y, -v.z);
        }
    }


}