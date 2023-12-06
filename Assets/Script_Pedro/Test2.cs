using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject paddle;
    [SerializeField]
    private Transform bottomWall;
    [SerializeField]
    private string inputAction = "Fire2";
    [SerializeField]
    private float forceMultiplier = 5f;
    
    private Test3 test3;
    private Rigidbody _rb;
    private int _countAction;
    private int _paddleHits;
    private Vector3 _pos1;
    private Vector3 _pos2;
    private Vector3 _posInitial;
    private enum TestPhase
    {
        FirstPhase, SecondPhase
    }
    private TestPhase _testPhase;
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _countAction = 0;
        _testPhase = TestPhase.FirstPhase;
        _paddleHits = 0;
        _pos1 = new Vector3(-0.556f, 0.7998f, 1.351f);
        _pos2 = new Vector3(0.556f, 0.7998f, 1.351f);
        var transform1 = transform;
        _posInitial = transform1.position;
        transform1.position = _pos1;
        test3 = GetComponent<Test3>();
    }

    // Update is called once per frame
    private void Update()
    {
        {
            if (_testPhase == TestPhase.FirstPhase)
            {
                if (Input.GetButtonDown(inputAction))
                {
                    _countAction++;
                    if (_countAction == 1)
                    {
                        var v = new Vector3(10f, 0, -20f) * forceMultiplier;
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
                        var v = new Vector3(-10f, 0, -20f) * forceMultiplier;
                        _rb.AddForce(v);
                        _countAction = 0;
                                        
                    }
                }
            }
        }
    }
    private void OnCollisionEnter(Collision c)
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
                    ResetGame(_posInitial);
                    Debug.Log("Test2 passed");
                    test3.CustomStart();
                    break;
            }
        }
    }
    
    private void ResetGame(Vector3 position)
    {
        transform.position = position;
        _rb.velocity = Vector3.zero;
    }

    public  void CustomStart()
    {
       print("Test 2 has begun");
    }
}
