using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Base : MonoBehaviour
{
    [SerializeField] private GameObject puck;

    private Test1 _test1;

    private Test2 _test2;
    // Start is called before the first frame update
    void Start()
    {
        _test1 = puck.GetComponent<Test1>();
        _test2 = puck.GetComponent<Test2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_test1.test1C)
        {
            _test1.enabled = false;
            // _test2.enabled = true;
        }
        else
        {
            _test1.enabled = true;
        }
    }
}
