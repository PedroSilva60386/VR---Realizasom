using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPaddleWithController : MonoBehaviour
{
    [SerializeField]
    private GameObject paddle;
    [SerializeField]
    private GameObject controller;
    
    private bool followController = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(followController)
        {
            Vector3 pp = paddle.transform.position;
            Vector3 cp = controller.transform.position;
            paddle.transform.position = new Vector3(cp.x, pp.y, pp.z);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (controller == null)
        {
            controller = other.gameObject;
            followController = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (controller != null && other.gameObject == controller)
        {
            controller = null;
            followController = false;
        }
    }
}
