using UnityEngine;
using UnityEngine.InputSystem;

public class Puck_Behaviour : MonoBehaviour
{
    public float speed = 5f;
    public string inputAction = "Fire1"; // Change to the desired action

    private bool puckStarted = false;

    void Update()
    {
        // Check if the specified action is triggered
        if (Input.GetButtonDown(inputAction) && !puckStarted)
        {
            StartMoving();
        }

        // Move the puck only if it has started
        if (puckStarted)
        {
            MovePuck();
        }
    }

    void StartMoving()
    {
        puckStarted = true;
        // Make the puck move forward along its local X-axis
        GetComponent<Rigidbody>().velocity = transform.right * speed;
    }

    void MovePuck()
    {
        // You can add additional logic here for continuous movement if needed
    }

    void OnCollisionEnter(Collision collision)
    {
        // You can add additional logic here for collisions if needed
    }
}