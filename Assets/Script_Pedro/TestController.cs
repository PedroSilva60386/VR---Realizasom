using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public Test1 test1;
    public Test2 test2;
    public Test3 test3;

    
    // Start is called before the first frame update
    private void Start()
    {
        test1.Awake();
        test1.Start();
        test1.Update();
        // test2.Awake();
        // test2.Start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
