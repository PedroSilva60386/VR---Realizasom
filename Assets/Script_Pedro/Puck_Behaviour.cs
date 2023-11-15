using UnityEngine;

public class ControlledMovePuck : MonoBehaviour
{
    public float speed = 5f;
    public OVRInput.Button startButton = OVRInput.Button.Two; // Change to the desired button

    private bool puckStarted = false;

    void Update()
    {
        // Check if the specified button is pressed
        if (OVRInput.GetDown(startButton))
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