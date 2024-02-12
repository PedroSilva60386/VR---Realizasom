using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject paddle;
    [SerializeField]
    private Transform goal;
    private GameObject puck;
    

    
    private Rigidbody _rb;
    private int _countAction;

    
    [CanBeNull] public event Action OnTestStart;
    [CanBeNull] public event Action<bool, string> OnTestEnd;
    
    
    public List<ITest> tests = new List<ITest>();

    public ITest currentTest;
    public int currentTestIndex;
    
    void ResetEverything(Vector3 position)
    {
        puck.transform.position = position;
        _rb.velocity = Vector3.zero;
        _countAction = 0;
    }
    
    void Start()
    {
        tests.Add(new FirstTest());
        tests.Add(new SecondTest());
        tests.Add(new ThirdTest());
        tests.Add(new FourthTest());
        currentTest = tests.First();
        currentTestIndex = 0;
        
        puck = gameObject;
        _rb = puck.GetComponent<Rigidbody>();
        _countAction = 0;
    }
    
    void StartTest()
    {
        _countAction++;
        if (_countAction == 1)
        {
            print(currentTest.GetTestName());
            OnTestStart?.Invoke();
            ResetEverything(currentTest.GetTestPosition());
            _rb.AddForce(currentTest.GetTestVelocity());
            _countAction = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
         StartTest();
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == paddle.name)
        {
            currentTest.Complete();
            ResetEverything(currentTest.GetTestPosition());
            Debug.Log(currentTest.GetTestName() + " passed");
            OnTestEnd?.Invoke(true, currentTest.GetTestName());
            if (!currentTest.HasFinished())
            {
                StartTest();
            }
            else {
                currentTestIndex++;
                currentTestIndex %= tests.Count;
                currentTest = tests[currentTestIndex];
            }
        }
        else if (c.gameObject.name == goal.name)
        {
            currentTest.Complete();
            ResetEverything(currentTest.GetTestPosition());
            Debug.Log(currentTest.GetTestName() + " failed");
            OnTestEnd?.Invoke(false, currentTest.GetTestName());
            if (!currentTest.HasFinished())
            {
                StartTest();
            }
            else {
                currentTestIndex++;
                currentTestIndex %= tests.Count;
                currentTest = tests[currentTestIndex];
            }
        }
    }
}
