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
    private Vector3 _pos1;
    private Vector3 _pos2;
    private Vector3 _pos3;
    private Vector3 _posInitial;
    private enum TestePhase
    {
        firstPhase, secondPhase, thirdPhase
    }

    private TestePhase testePhase;


    // Start is called before the first frame update
    private void Start()
    { 
        _rb = GetComponent<Rigidbody>();
        _countAction = 0;
        testePhase = TestePhase.firstPhase;
        _paddleHits = 0;
        _pos1 = new Vector3(-0.556f, 0.7998f, 1.351f);
        _pos2 = new Vector3(0.045f, 0.7998f, 1.351f);
        _pos3 = new Vector3(0.556f, 0.7998f, 1.351f);
        _posInitial = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (testePhase == TestePhase.firstPhase)
        {
            if (Input.GetButtonDown(inputAction))
            {
                //transform.position = _pos1;
                _countAction++;
                if (_countAction == 2)
                {
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;

                }
            }
        }
        else if (testePhase == TestePhase.secondPhase)
        {
            if (Input.GetButtonDown(inputAction))
            {
                //transform.position = _pos2;
                //_rb.velocity = Vector3.zero;
                _countAction++;
                if (_countAction == 2)
                {
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;
                                
                }
            }
        }
        else if (testePhase == TestePhase.thirdPhase)
        {
            if (Input.GetButtonDown(inputAction))
            {
                //transform.position = _pos3;
                _countAction++;
                if (_countAction == 2)
                {
                    var v = new Vector3(0, 0, -20f) * forceMultiplier;
                    _rb.AddForce(v);
                    _countAction = 0;
             
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
                    testePhase = TestePhase.firstPhase;
                    ResetGame(_pos2);
                    Debug.Log("Test1 passed");
                    break;
                case 2:
                    testePhase = TestePhase.secondPhase;
                    ResetGame(_pos3);
                    Debug.Log("Test2 passed");
                    break;
                case 3:
                    testePhase = TestePhase.thirdPhase;
                    ResetGame(_posInitial);
                    Debug.Log("Test3 passed");
                    break;
            }
        }
    }
    
     private void ResetGame(Vector3 position)
     {
         transform.position = position;
         _rb.velocity = Vector3.zero;
     }
}
