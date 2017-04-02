using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompressibleCube : MonoBehaviour {

    public float speed;

    private Vector3 _iPosition;
    private Quaternion _iRotation;
    private bool _colliding;

    void Start()
    {
        _iPosition = transform.position;
        _iRotation = transform.rotation;
        _colliding = false;
        // gameObject.SetActive(false);
    }

    void Update()
    {
        if (_colliding) return;
        float t = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, _iPosition, t);
        transform.rotation = Quaternion.Lerp(transform.rotation, _iRotation, t);
    }

    void OnCollisionEnter()
    {
        _colliding = true;
    }

    void OnCollisionExit()
    {
        _colliding = false;
    }

    public void ToggleFloatingCube(Toggle toggle)
    {
        gameObject.SetActive(toggle.isOn);
        if (toggle.isOn)
        {
            
        }
        else
        {
            _colliding = false;
            transform.position = _iPosition;
            transform.rotation = _iRotation;
        }
    }
}
