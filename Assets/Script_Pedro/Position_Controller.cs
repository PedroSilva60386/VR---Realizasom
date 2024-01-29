using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class Position_Controller : MonoBehaviour
{
    [SerializeField] private GameObject puck;
    [SerializeField] private GameObject pong;

    private Rigidbody _pongRb;
    private Rigidbody _puckRb;
    private List<Vector3> _pongPosition;
    private List<Vector3> _puckPosition;
    private List<bool> _success;
    private Puck_Behaviour _puckBehaviour;
    private Test1 _test1;
    private string _filePath;
    private bool _hasFileBeenWritten;
    private float _testDuration;
    private bool _isTestRunning = false;
     

    // Start is called before the first frame update
    void Start()
    {
        _testDuration = 0;
        _puckBehaviour = puck.GetComponent<Puck_Behaviour>();
        _test1 = puck.GetComponent<Test1>();
        
        _pongRb = pong.GetComponent<Rigidbody>();
        _puckRb = puck.GetComponent<Rigidbody>();

        _pongPosition = new List<Vector3>();
        _puckPosition = new List<Vector3>();
        _success = new List<bool>();
        _hasFileBeenWritten = false;

        _test1.OnTestStart += () => _isTestRunning = true;
        _test1.OnTestEnd += OnTestEnd;


        // Specify the file path where the CSV file will be saved
        //_filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "game_data.csv");
    }
    
    
    private void OnTestEnd()
    {
        print("On Test End. Writing file..");
        ComputeFileName();
        var success = false;
        if (_puckBehaviour._collisionBotSide) success = false;
        if (_puckBehaviour._colisionPaddle) success = true;
        SaveDataToCSV(success);
        ResetAll();

    }

    private void ComputeFileName()
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
        switch(_test1._testPhase)
        {
            case Test1.TestPhase.FirstPhase:
                _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "game_data_1_" + timestamp + ".csv");
                break;
            case Test1.TestPhase.SecondPhase:
                _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "game_data_2_" + timestamp + ".csv");
                break;
            case Test1.TestPhase.ThirdPhase:
                _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "game_data_3_" + timestamp + ".csv");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ResetAll()
    {
        _pongPosition.Clear();
        _puckPosition.Clear();
        _success.Clear();
        _testDuration = 0;
        _hasFileBeenWritten = false;
        _isTestRunning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isTestRunning)
        {
            _testDuration += Time.fixedTime;
            _pongPosition.Add(_pongRb.position);
            _puckPosition.Add(_puckRb.position);
        }

    }
    // Save the data to a CSV file when needed
    void SaveDataToCSV(bool success)
    {
        if (_hasFileBeenWritten)
            return;
        
        using (var sw = new StreamWriter(_filePath))
        {
            // Write header
            sw.WriteLine("Pong_Position_X,Puck_Position_X,Puck_Position_Z,Success,Duration(sec)");

            // Write data
            for (var i = 0; i < _pongPosition.Count; i++)
            {
                var line = $"{_pongPosition[i].x}," +
                           $"{_puckPosition[i].x},{_puckPosition[i].z},{success},{_testDuration}";
                sw.WriteLine(line);
            }
        }
        _hasFileBeenWritten = true;
    }
}