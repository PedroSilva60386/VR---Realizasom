using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Paddle : MonoBehaviour
{
    [SerializeField] private GameObject puck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        transform.position = new Vector3(puck.transform.position.x, 0.804f,1.63f);
    }
}
