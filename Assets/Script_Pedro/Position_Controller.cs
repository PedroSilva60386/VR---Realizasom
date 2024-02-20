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

    private Rigidbody _pongRb; //Pong rigidBody
    private Rigidbody _puckRb; //Puck rigidBody
    private List<Vector3> _pongPosition;
    private List<Vector3> _puckPosition;
    private List<bool> _success; // To save if the test was a success or not
    private List<float> _timePassed; //Time of a test
    private TestManager _testManager;
    private string _filePath; //Path where we want the file to be written in
    private bool _hasFileBeenWritten;
    private bool _isTestRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _testManager = puck.GetComponent<TestManager>();
        
        _pongRb = pong.GetComponent<Rigidbody>();
        _puckRb = puck.GetComponent<Rigidbody>();

        _pongPosition = new List<Vector3>();
        _puckPosition = new List<Vector3>();
        _success = new List<bool>();
        _timePassed = new List<float>();
        _hasFileBeenWritten = false;

        _testManager.OnTestStart += () => _isTestRunning = true;
        _testManager.OnTestEnd += OnTestEnd;


        // Specify the file path where the CSV file will be saved
        //_filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "game_data.csv");
    }
    
    //What needs to be done when the test ends
    private void OnTestEnd(bool won, string testName)
    {
        print("On Test End. Writing file..");
        ComputeFileName(won, testName);
        SaveDataToCSV(won);
        ResetAll();

    }
    
    //Used to create the name of the file and to create the path
    private void ComputeFileName(bool success, string testName)
    {
        var folderPath = @"C:\Users\fadjo\OneDrive\Ambiente de Trabalho\Pedro\Estagio\Testes";
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss_");
        _filePath = Path.Combine(folderPath, testName + timestamp + "Success =" + success + ".csv");
    }

    //Reset the variables
    private void ResetAll()
    {
        _isTestRunning = false;
        _pongPosition.Clear();
        _puckPosition.Clear();
        _success.Clear();
        _timePassed.Clear();
        _hasFileBeenWritten = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isTestRunning)
        {
            _timePassed.Add(Time.fixedTime);
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
            sw.WriteLine("Pong_Position_X.Puck_Position_X.Puck_Position_Z.Success.Duration(sec)");

            // Write data
            for (var i = 0; i < _pongPosition.Count; i++)
            {
                var line = $"{_pongPosition[i].x}." +
                           $"{_puckPosition[i].x}." + $"{_puckPosition[i].z}." + $"{success}." + $"{_timePassed[i]}";
                sw.WriteLine(line);
            }
        }
        _hasFileBeenWritten = true;
    }
}