using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Actuator : MonoBehaviour
{
    public int pin;
    //[SerializeField]
    [Range(0.0f, 1.0f)]
    public float Value;
    public HapticDeviceController device;

    float _oldValue;

    void Start ()
    {
        GameObject sceneController = GameObject.Find("/HapticDeviceController");
        if (sceneController)
        {
            device = sceneController.GetComponent<HapticDeviceController>();
        }
    }

    void Update ()
    {
        if (_oldValue != Value)
        {
            OnChange();
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
