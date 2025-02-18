using System;
using System.IO.Ports;
using UnityEngine;

public class ArduinoLink : MonoBehaviour {
    public string portName = "COM3";
    public int baudRate = 9600;

    private SerialPort serialPort;

    public static ArduinoLink instance;

    public float gyroX, gyroY, gyroZ;
    public float joyX, joyY;
    public bool button1, button2, button3, joyButton;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Garde la connexion entre les scènes
        } else {
            Destroy(gameObject);
        }
    }

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
            string[] trimedData = data.Split(' ');
            // 0 : button1
            // 1 : button2
            // 2 : button3
            // 3 : gyroX
            // 4 : gyroY
            // 5 : gyroZ
            // 6 : joy button
            // 7 : joyX
            // 8 : joyY


            if (trimedData.Length >= 9) {
                button1 = trimedData[0] == "1";
                button2 = trimedData[1] == "1";
                button3 = trimedData[2] == "1";
                gyroX = float.Parse(trimedData[3]);
                gyroY = float.Parse(trimedData[4]);
                gyroZ = float.Parse(trimedData[5]);
                joyButton = trimedData[6] == "1";
                joyX = float.Parse(trimedData[7]);
                joyY = float.Parse(trimedData[8]);
            }
        }
    }

    private void OnApplicationQuit() {
        if (serialPort != null && serialPort.IsOpen) {
            serialPort.Close();
        }
    }
}