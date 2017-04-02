using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Actuator : MonoBehaviour
{
    public int pin;
    // Usado por el HapticEditor para mantener el valor del color
    public Color Color = Color.white;
    //[SerializeField]
    // [Range(-1.5f, 1.5f)]
    [Range(0.0f, 1.0f)]
    public float Value;
    public HapticDeviceController device;

    float _oldValue;

    void Update ()
    {
        if (_oldValue != Value)
        {
            OnChange();
            Color = Color.Lerp(Color.white, Color.red, Value / 1.0f);
            // if (Mathf.Sign(Value) == 1)
            // {
            //     Color = Color.Lerp(Color.white, Color.red, Value / 1.5f);
            // }
            // else
            // {
            //     Color = Color.Lerp(Color.white, Color.blue, Mathf.Abs(Value) / 1.5f);
            // }
        }
        _oldValue = Value;
    }

    void OnDisable()
    {
        Value = 0f;
    }

    void OnChange()
    {
        if (device != null && pin != 0)
        {
            int conversion = (int)(Mathf.Clamp(Value, 0f, 1f) * 255f);
            device.UpdatePin(pin, conversion);
        }
    }
}
