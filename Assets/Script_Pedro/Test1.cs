using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    private GameObject paddle;
    [SerializeField]
    private Transform bottomWall;
    [SerializeField]
    private string inputAction = "Fire2";
    [SerializeField]
    private float forceMultiplier = 5f;

    private Rigidbody _rb;
    private int _countAction;
    private int _paddleHits;
    private bool _test1;
    private bool _test2;
    private bool _test3;
    
    
    
    // Start is called before the first frame update
    private void Start()
    { 
        _rb = GetComponent<Rigidbody>();
        _countAction = 0;
        _test1 = false;
        _test2 = false;
        _test3 = false;
        _paddleHits = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_test1)
        {
            if (Input.GetButtonDown(inputAction))
            {
                transform.position = new Vector3(-0.556f, 0.7998f, 1.351f);
                _countAction++;
                print("First action " +  _countAction);
                if (_countAction == 2)
                {
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;
                    print("Second action" +  _countAction);
                    print(transform.position);

                }
            }
        }
        if (_test1 && !_test2)
        {
            transform.position = new Vector3(0.045f, 0.7998f, 1.351f);
            _countAction++;
            print("First action 2 " +  _countAction);
            if (_countAction == 2)
            {
                var v = new Vector3(0, 0, -20f) * forceMultiplier;
                _rb.AddForce(v);
                _countAction = 0;
                print("Second action 2 " +  _countAction);
                print(transform.position);
                
            }
        }
        if (_test1 && _test2 && !_test3)
        {
            transform.position = new Vector3(0.556f, 0.7998f, 1.351f);
            _countAction++;
            if (_countAction == 2)
            {
                var v = new Vector3(0, 0, -20f) * forceMultiplier;
                _rb.AddForce(v);
                _countAction = 0;
                
            }
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == bottomWall.name)
        {
            ResetGame();
        }

        if (c.gameObject.name == paddle.name)
        {
            _paddleHits++;
            if (_paddleHits == 1)
            {
                _test1 = true;
                ResetGame();
                Debug.Log("Test1 passed");
            }
            if (_paddleHits == 2)
            {
                _test2 = true;
                ResetGame();
                Debug.Log("Test2 passed");
            }
            if (_paddleHits == 3)
            {
                _test3 = true;
                ResetGame();
                Debug.Log("Test3 passed");
            }
            
        }
        
        
       
    }
    
     private void ResetGame()
     {
         transform.position = new Vector3(-0.556f, 0.7998f, 1.544f);;
         _rb.velocity = Vector3.zero;
    
     }
}
