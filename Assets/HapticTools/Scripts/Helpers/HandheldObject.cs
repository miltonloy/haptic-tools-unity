using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HandheldObject : MonoBehaviour {

    private Transform _anchor;
    private Rigidbody _rigidbody;
    private bool _stateChanged = false;
    private PinchDetector _pinch;

    void Start() {
      // Acá va el anchor este que no se para que lo usa
      GameObject pinchControl = new GameObject("Grab Anchor");
      _anchor = pinchControl.transform;
      _anchor.transform.parent = transform.parent;
      transform.parent = _anchor;

      _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
      if (_stateChanged)
      {
        transform.SetParent(null, true);
      }

      if (_pinch != null)
      {
        // _rigidbody.isKinematic = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transformSingleAnchor(_pinch);
      } else {
        // _rigidbody.isKinematic = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
      }

      if (_stateChanged)
      {
        transform.SetParent(_anchor, true);
        _stateChanged = false;
      }
    }

    public void PinchEnabled(PinchDetector pinch)
    {
      _pinch = pinch;
      _stateChanged = true;
    }

    public void PinchDisabled()
    {
      _pinch = null;
      _stateChanged = true;
    }

    private void transformSingleAnchor(PinchDetector singlePinch) {
      _anchor.position = singlePinch.Position;
      _anchor.rotation = singlePinch.Rotation;
      _anchor.localScale = Vector3.one;
    }
}
