using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetractableObject : MonoBehaviour {

    public float speed = 2.0f;

    private Vector3 _iPosition;
    private Quaternion _iRotation;
    private bool _colliding;

    void Start()
    {
        _iPosition = transform.localPosition;
        _colliding = false;
    }

    void Update()
    {
        if (_colliding) return;
        float t = speed * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _iPosition, t);
    }

    void OnCollisionEnter()
    {
        _colliding = true;
    }

    void OnCollisionExit()
    {
        _colliding = false;
    }

}
