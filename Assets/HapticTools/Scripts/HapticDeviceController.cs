using UnityEngine;
using wbadry;

public class HapticDeviceController : MonoBehaviour {

    public string portName = "COM5"; // En Mac es /dev/cu.usbmodem1441
    public int baudRate = 57600;
    //[Range(0, 255)]
    //public int value;

    bool _connected = false;

    ArduinoUno _firmata;

    public void UpdatePin(int pin, int value)
    {
        if (!_connected) return;
        value = (int)Mathf.Clamp(value, 0, 255);
        _firmata.pinMode(pin, 3);
        _firmata.analogWrite(pin, value);
        //Debug.Log("analogWrite pin " + pin + " value " + value);
    }

    void Start () {
        _firmata = new ArduinoUno(portName, baudRate);
        try
        {
            _firmata.Open();
        }
        catch (System.IO.IOException)
        {
            Debug.Log("No se puede conectar a arduino en puerto "+portName);
            return;
        }
        _connected = true;
        Debug.Log("Conectado a arduino en puerto: " + portName + " con baudRate: " + baudRate);
    }

    void OnApplicationQuit()
    {
        if (!_connected) return;
        _firmata.Close();
    }
}
