using System;
using System.IO.Ports;
using UnityEngine;

public class ArduinoLink : MonoBehaviour {
    public string portName = "COM3";
    public int baudRate = 9600;

    private SerialPort serialPort;

    void OnEnable() {
        serialPort = new SerialPort(portName, baudRate);

        try {
            if(!serialPort.IsOpen) {
                serialPort.Open();
                Debug.Log("Connexion successful!");
            }
        }catch(System.Exception e) {
            Debug.LogError($"Connexion failed! {e.Message}");
        }
    }

    void Update() {
        if(serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0) {
            string data = serialPort.ReadLine();

            Debug.Log(data);
        }
    }
}