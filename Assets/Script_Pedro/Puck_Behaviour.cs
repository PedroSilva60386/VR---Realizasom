using System;using Oculus.Interaction.Input;using OVR;using TMPro;using UnityEngine;using UnityEngine.InputSystem.XR.Haptics;using UnityEngine.XR;using InputDevice = UnityEngine.InputSystem.InputDevice;using Random = UnityEngine.Random;public class Puck_Behaviour : MonoBehaviour{    //Table Walls    [SerializeField]    private Transform leftWall;    [SerializeField]    private Transform rightWall;    [SerializeField]    private Transform topWall;    [SerializeField]    private Transform bottomWall;    [SerializeField]     private Transform goal;    [SerializeField]    private float forceMultiplier = 5f;    [SerializeField]    private string inputAction = "Fire1"; // Change to the desired action    [SerializeField]    private GameObject paddle;    [SerializeField]    private AudioSource bump;    [SerializeField]    private AudioSource wind;    [SerializeField]    private GameObject botPaddle;    [SerializeField]    private float playerForce;        //Puck's Rigid Body component    private Rigidbody _rb;            //Player force applied to puck            private void Start()    {        _rb = GetComponent<Rigidbody>();    }        //Puck's first velocity value, and friction behaviour    private void FixedUpdate()    {                if (Input.GetButtonDown(inputAction))        {            var v = new Vector3(Random.Range(0f,0.5f), 0,Random.Range(0f,0.5f) ) * forceMultiplier;            _rb.AddForce(v);        }        var vv = _rb.velocity;        _rb.velocity = vv * 0.999f;        ChangePitch();        ChangePan();    }    private void OnDrawGizmos()    {             //    var worldMaxPos = ( rightWall.position);     //    var worldMinPos = ( leftWall.position);     //    var worldPuckPosition = (transform.position);     //    Gizmos.DrawSphere(worldPuckPosition, 1.0f);     //   Gizmos.DrawSphere(worldMaxPos, 0.3f);     //   Gizmos.DrawSphere(worldMinPos, 0.3f);    }    private void ChangePan()    {        var worldMaxPos = (rightWall.position).x;        var worldMinPos = ( leftWall.position).x;        var worldPuckPosition = ( transform.position).x;        var distanceWalls = Math.Abs(worldMaxPos - worldMinPos);             var distancePaddle = worldMaxPos - worldPuckPosition;        var puckX01 = distancePaddle / distanceWalls;              puckX01 = Mathf.Max(puckX01, 0.0f);        puckX01 = Mathf.Min(puckX01, 1.0f);        wind.panStereo = Mathf.Lerp(1.0f,-1.0f,puckX01);    }    // ReSharper disable Unity.PerformanceAnalysis    private void ChangePitch()    {        var worldMaxPos = ( topWall.position).z;        var worldMinPos = ( bottomWall.position).z;        var worldPuckPosition = ( transform.position).z;        var distanceWalls = Math.Abs(worldMaxPos - worldMinPos);        var distancePaddle = worldMaxPos - worldPuckPosition;        var puckZ01 = distancePaddle / distanceWalls;        puckZ01 = Mathf.Max(puckZ01, 0.0f);        puckZ01 = Mathf.Min(puckZ01, 1.0f);        wind.pitch = Mathf.Lerp(0.5f, 1.5f, puckZ01);    }    //Collision Detection    private void OnCollisionEnter(Collision collision)    {        Debug.Log("Entered collision with " + collision.gameObject.name);        if (collision.gameObject.name == rightWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(-v.x, v.y, v.z);            bump.Play();        }                if (collision.gameObject.name == leftWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(-v.x, v.y, v.z);            bump.Play();        }                        if (collision.gameObject.name == bottomWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(v.x, v.y, -v.z);            bump.Play();        }                        if (collision.gameObject.name == topWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(v.x, v.y, -v.z);            bump.Play();        }                // ReSharper disable once InvertIf        if (collision.gameObject.name == paddle.gameObject.name)        {            var ps = paddle.gameObject.GetComponent<AudioSource>();            ps.Play();            var contact = collision.contacts[0];            var reflectedVelocity = Vector3.Reflect(_rb.velocity, contact.normal);            var v = _rb.velocity;            _rb.velocity = reflectedVelocity * playerForce;            reflectedVelocity.y = 0f;            HapticFeedBack();        }        if (collision.gameObject.name == botPaddle.gameObject.name)        {            var ps = botPaddle.gameObject.GetComponent<AudioSource>();            ps.Play();            var contact = collision.contacts[0];            var reflectedVelocity = Vector3.Reflect(_rb.velocity, contact.normal);            var v = _rb.velocity;            _rb.velocity = reflectedVelocity * playerForce;            reflectedVelocity.y = 0f;            HapticFeedBack();        }        if (collision.gameObject.name == goal.gameObject.name)        {            //Audio Source da bottom Wall            var ba = goal.gameObject.GetComponent<AudioSource>();            ba.Play();            ResetGame();        }    }    private static void HapticFeedBack()    {        var rightDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);        rightDevice.SendHapticImpulse(0, 0.5f, 0.25f);        var leftDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);        leftDevice.SendHapticImpulse(0, 0.5f, 0.25f);    }    private void ResetGame()    {        transform.position = new Vector3(0,0.8f,0);        _rb.velocity = Vector3.zero;        }}