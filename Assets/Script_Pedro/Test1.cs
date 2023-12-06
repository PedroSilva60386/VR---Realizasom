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
    [SerializeField] 
    private GameObject puck;
    
    private Rigidbody _rb;
    private int _countAction;
    private int _paddleHits;
    private Vector3 _pos1;
    private Vector3 _pos2;
    private Vector3 _pos3;
    private Vector3 _posInitial;
    private enum TestPhase
    {
        FirstPhase, SecondPhase, ThirdPhase
    }

    private TestPhase _testPhase;


    // Start is called before the first frame update

    public void Awake()
    {
        print("Test 1 has started");
    }

    public void Start()
    { 
        _rb = puck.GetComponent<Rigidbody>();
        _countAction = 0;
        _testPhase = TestPhase.FirstPhase;
        _paddleHits = 0;
        _pos1 = new Vector3(-0.556f, 0.7998f, 1.351f);
        _pos2 = new Vector3(0.045f, 0.7998f, 1.351f);
        _pos3 = new Vector3(0.556f, 0.7998f, 1.351f);
        var transform1 = puck.transform;
        _posInitial = transform1.position;
        transform1.position = _pos1;
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
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
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
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;
                                
                }
            }
        }
        else if (_testPhase == TestPhase.ThirdPhase)
        {
            if (Input.GetButtonDown(inputAction))
            {
                _countAction++;
                if (_countAction == 1)
                {
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;
             
                }
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == bottomWall.name)
        {
            ResetGame(_posInitial);
        }

        if (c.gameObject.name == paddle.name)
        {
            _paddleHits++;
            print(_paddleHits);
            switch (_paddleHits)
            {
                case 1:
                    _testPhase = TestPhase.FirstPhase;
                    ResetGame(_pos2);
                    Debug.Log("Test1 passed");
                    break;
                case 2:
                    _testPhase = TestPhase.SecondPhase;
                    ResetGame(_pos3);
                    Debug.Log("Test2 passed");
                    break;
                case 3:
                    _testPhase = TestPhase.ThirdPhase;
                    ResetGame(_posInitial);
                    Debug.Log("Test3 passed");
                    break;
            }
        }
    }
    
     private void ResetGame(Vector3 position)
     {
         puck.transform.position = position;
         _rb.velocity = Vector3.zero;
     }
}
