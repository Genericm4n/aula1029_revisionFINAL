using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoController : MonoBehaviour
{
    #region VARIABLES

    [Header("ARDUINO SETTING")]
    private SerialPort arduino;
    public string portName = "COM3";
    public int baudRate = 9600;
    private string backforwardsRead = "0";

    [Header("CUBE SETTINGS")]
    public MeshRenderer cube01;
    public MeshRenderer cube02;
    public MeshRenderer cube03;
    private bool backforwardsKeyPress = false;

    #endregion VARIABLES

    private void Start()
    {
        try
        {
            arduino = new SerialPort(portName, baudRate) { ReadTimeout = 200 };

            arduino.Open();
        }
        catch (System.Exception) { }
    }

    private void Update()
    {
        //var read = arduino.ReadLine();
        var read = SimulateReadLine();

        var keyDown = read == "1" && backforwardsRead == "0";
        var keyPress = read == "1";
        var keyUp = read == "0" && backforwardsRead == "1";

        // process keyDown - Press - Up
        if (keyDown)
        {
            ChangeCubeColor(cube01);
            StartCoroutine(DeleteCube(cube01));
        }

        if (keyPress)
        {
            ChangeCubeColor(cube02);
        }
        else if (backforwardsKeyPress)
        {
            StartCoroutine(DeleteCube(cube02));
        }
        backforwardsKeyPress = keyPress;

        if (keyUp)
        {
            ChangeCubeColor(cube03);
            StartCoroutine(DeleteCube(cube03));
        }

        backforwardsRead = read;
    }

    private string SimulateReadLine()
    {
        return Input.GetKey(KeyCode.Z) ? "1" : "0";
    }

    private void ChangeCubeColor(MeshRenderer cube)
    {
        cube.material.color = Color.red;
    }

    private IEnumerator DeleteCube(MeshRenderer cube)
    {
        yield return new WaitForSeconds(0.25f);

        cube.material.color = Color.white;
    }
}