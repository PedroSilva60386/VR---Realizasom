using System;using Oculus.Interaction.Input;using OVR;using TMPro;using UnityEngine;using UnityEngine.InputSystem.XR.Haptics;using UnityEngine.XR;using InputDevice = UnityEngine.InputSystem.InputDevice;using Random = UnityEngine.Random;public class Puck_Behaviour : MonoBehaviour{    //Table Walls    [SerializeField]    private Transform leftWall;    [SerializeField]    private Transform rightWall;    [SerializeField]    private Transform topWall;    [SerializeField]    private Transform bottomWall;    [SerializeField]    private float forceMultiplier = 5f;    [SerializeField]    private string inputAction = "Fire1"; // Change to the desired action    [SerializeField]    private GameObject paddle;    [SerializeField]    private AudioSource bump;    [SerializeField]    private AudioSource wind;        //Puck's Rigid Body component    private Rigidbody _rb;            //Player force applied to puck    private const float PlayerForce = 1.5f;        private void Start()    {        _rb = GetComponent<Rigidbody>();    }        //Puck's first velocity value, and friction behaviour    private void FixedUpdate()    {                if (Input.GetButtonDown(inputAction))        {            var v = new Vector3(Random.value, 0, Random.value) * forceMultiplier;            _rb.AddForce(v);        }        var vv = _rb.velocity;        _rb.velocity = vv * 0.999f;        ChangePitch();    }    // ReSharper disable Unity.PerformanceAnalysis    private void ChangePitch()    {        var worldMaxPos = (topWall.localToWorldMatrix * topWall.position).z;        var worldMinPos = (bottomWall.localToWorldMatrix * bottomWall.position).z;        var worldPuckPosition = (transform.localToWorldMatrix * transform.position).z;        var distanceWalls = Math.Abs(worldMaxPos - worldMinPos);        var distancePaddle = worldMaxPos - worldPuckPosition;        var puckZ01 = distancePaddle / distanceWalls;        puckZ01 = Mathf.Max(puckZ01, 0.0f);        puckZ01 = Mathf.Min(puckZ01, 1.0f);        wind.pitch = Mathf.Lerp(0.5f, 1.5f, puckZ01);    }    //Collision Detection    private void OnCollisionEnter(Collision collision)    {        Debug.Log("Entered collision with " + collision.gameObject.name);        if (collision.gameObject.name == rightWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(-v.x, v.y, v.z);            bump.Play();        }                if (collision.gameObject.name == leftWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(-v.x, v.y, v.z);            bump.Play();        }                        if (collision.gameObject.name == bottomWall.gameObject.name)        {            //Audio Source da bottom Wall            var ba = bottomWall.gameObject.GetComponent<AudioSource>();            ba.Play();            ResetGame();        }                        if (collision.gameObject.name == topWall.gameObject.name)        {            var v = _rb.velocity;            _rb.velocity = new Vector3(v.x, v.y, -v.z);            bump.Play();        }                // ReSharper disable once InvertIf        if (collision.gameObject.name == paddle.gameObject.name)        {            var ps = paddle.gameObject.GetComponent<AudioSource>();            ps.Play();            var v = _rb.velocity;            _rb.velocity = new Vector3(-v.x, v.y, -v.z) * PlayerForce;            HapticFeedBack();        }    }    private void HapticFeedBack()    {        var device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);        device.SendHapticImpulse(0, 0.5f, 0.25f);    }    //Used to reset the puck to it's initial state    private void ResetGame()    {        transform.position = new Vector3(0,(float)0.7998,0);        _rb.velocity = Vector3.zero;    }}