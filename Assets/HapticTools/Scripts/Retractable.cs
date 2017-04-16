using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Retractable : MonoBehaviour {

    public float speed = 2.0f;

    private Vector3 _iPosition;
    private Quaternion _iRotation;
    private bool _colliding;

    void Start()
    {
        _iPosition = transform.localPosition;
        // _iRotation = transform.rotation;
        _colliding = false;
    }

    void Update()
    {
        if (_colliding) return;
        float t = speed * Time.deltaTime;
        // transform.position = Vector3.Lerp(transform.position, _iPosition, t);
        // transform.rotation = Quaternion.Lerp(transform.rotation, _iRotation, t);
        transform.localPosition = Vector3.Lerp(transform.localPosition, _iPosition, t);
        // transform.rotation = Quaternion.Lerp(transform.rotation, _iRotation, t);
    }

    void OnCollisionEnter()
    {
        _colliding = true;
    }

    void OnCollisionExit()
    {
        _colliding = false;
    }

    // public void ToggleFloatingCube(Toggle toggle)
    // {
    //     gameObject.SetActive(toggle.isOn);
    //     if (toggle.isOn)
    //     {
            
    //     }
    //     else
    //     {
    //         _colliding = false;
    //         transform.position = _iPosition;
    //         transform.rotation = _iRotation;
    //     }
    // }
}
