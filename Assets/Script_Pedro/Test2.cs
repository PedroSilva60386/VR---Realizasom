using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Test2
{
    private GameObject paddle;
    private Transform goal;
    private string inputAction = "Fire2";
    private float forceMultiplier = 5f; 
    private GameObject puck;
    
    private Rigidbody _rb;
    private int _countAction;
    private int _paddleHits;
    private Vector3 _pos1;
    private Vector3 _pos2;
    private Vector3 _posInitial;
    public bool test2A;
    public bool test2B;

    [CanBeNull] public event Action OnTestStart;
    [CanBeNull] public event Action OnTestEnd;

    public enum TestPhase
    {
        FirstPhase, SecondPhase
    }

    public TestPhase _testPhase;
    

    public void Start()
    { 
        _rb = puck.GetComponent<Rigidbody>();
        _countAction = 0;
        _testPhase = TestPhase.FirstPhase;
        _paddleHits = 0;
        _pos1 = new Vector3(-0.556f, 0.7998f, 1.351f);
        _pos2 = new Vector3(0.556f, 0.7998f, 1.351f);
        var transform1 = puck.transform;
        _posInitial = transform1.position;
        transform1.position = _pos1;
        test2A = false;
        test2B = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (_testPhase == TestPhase.FirstPhase)
        {
            if (Input.GetButtonDown(inputAction))
            {
                _countAction++;
                if (_countAction == 1)
                {
                    OnTestStart?.Invoke();
                    var v = new Vector3(-40f, 0, -20f) * forceMultiplier ;
                    _rb.AddForce(v);
                    _countAction = 0;

                }
            }
        }
        else if (_testPhase == TestPhase.SecondPhase)
        {
            if (Input.GetButtonDown(inputAction))
            {
                _countAction++;
                if (_countAction == 1)
                {
                        OnTestStart?.Invoke();
                    var v = new Vector3(40f, 0, -20f)  * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;
                                
                }
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == paddle.name)
        {
            _paddleHits++;
            switch (_paddleHits)
            {
                case 1:
                    test2A = true;
                    _testPhase = TestPhase.FirstPhase;
                    ResetGame(_pos2);
                    Debug.Log("Test1 passed");
                    OnTestEnd?.Invoke();
                    break;
                case 2:
                    test2B = true;
                    _testPhase = TestPhase.SecondPhase;
                    ResetGame(_posInitial);
                    Debug.Log("Test2 passed");
                    OnTestEnd?.Invoke();
                    break;
            }

            test2A = false;
            test2B = false;
        }
        else if (c.gameObject.name == goal.name)
        {
            switch (_paddleHits)
            {
                case 0:
                    test2A = false;
                    _testPhase = TestPhase.FirstPhase;
                    ResetGame(_pos1);
                    Debug.Log("Test1 Failed");
                    OnTestEnd?.Invoke();
                    break;
                case 1:
                    test2B = false;
                    _testPhase = TestPhase.SecondPhase;
                    Debug.Log("Test2 Failed");
                    ResetGame(_pos2);
                    OnTestEnd?.Invoke();
                    break;  
            }
        }
    }
    
     private void ResetGame(Vector3 position)
     {
         puck.transform.position = position;
         _rb.velocity = Vector3.zero;
         _countAction = 0;
     }
}
