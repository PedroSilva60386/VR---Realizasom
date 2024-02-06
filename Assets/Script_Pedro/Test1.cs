using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    private GameObject paddle;
    [SerializeField]
    private Transform goal;
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
    public bool test1A;
    public bool test1B;
    public bool test1C;

    [CanBeNull] public event Action OnTestStart;
    [CanBeNull] public event Action OnTestEnd;

    public enum TestPhase
    {
        FirstPhase, SecondPhase, ThirdPhase
    }

    public TestPhase _testPhase;


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
        test1A = false;
        test1B = false;
        test1C = false;
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
                    var v = new Vector3(0, 0, -20f) * forceMultiplier ;
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
                    var v = new Vector3(0, 0, -20f)  * forceMultiplier;
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
                    OnTestStart?.Invoke();
                    var v = new Vector3(0, 0, -20f)  * forceMultiplier;
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
                    test1A = true;
                    _testPhase = TestPhase.FirstPhase;
                    ResetGame(_pos2);
                    Debug.Log("Test1 passed");
                    OnTestEnd?.Invoke();
                    break;
                case 2:
                    test1B = true;
                    _testPhase = TestPhase.SecondPhase;
                    ResetGame(_pos3);
                    Debug.Log("Test2 passed");
                    OnTestEnd?.Invoke();
                    break;
                case 3: 
                    test1C = true;
                    _testPhase = TestPhase.ThirdPhase;
                    ResetGame(_posInitial);
                    Debug.Log("Test3 passed");
                    OnTestEnd?.Invoke();
                    break;
            }

            test1A = false;
            test1B = false;
            test1C = false;
        }
        else if (c.gameObject.name == goal.name)
        {
            switch (_paddleHits)
            {
                case 0:
                    test1A = false;
                    _testPhase = TestPhase.FirstPhase;
                    ResetGame(_pos1);
                    Debug.Log("Test1 Failed");
                    OnTestEnd?.Invoke();
                    break;
                case 1:
                    test1B = false;
                    _testPhase = TestPhase.SecondPhase;
                    Debug.Log("Test2 Failed");
                    ResetGame(_pos2);
                    OnTestEnd?.Invoke();
                    break;
                case 2:
                    test1C = false;
                    _testPhase = TestPhase.ThirdPhase;
                    ResetGame(_pos3);
                    Debug.Log("Test3 Failed");
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
