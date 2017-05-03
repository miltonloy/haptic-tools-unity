using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HandheldObject : MonoBehaviour {

   public enum RotationMethod {
      None,
      Single,
      Full
    }

    [SerializeField]
    private RotationMethod _oneHandedRotationMethod;

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
        _rigidbody.isKinematic = true;
        transformSingleAnchor(_pinch);
      } else {
        _rigidbody.isKinematic = false;
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

      switch (_oneHandedRotationMethod) {
        case RotationMethod.None:
          break;
        case RotationMethod.Single:
          Vector3 p = singlePinch.Rotation * Vector3.right;
          p.y = _anchor.position.y;
          _anchor.LookAt(p);
          break;
        case RotationMethod.Full:
          _anchor.rotation = singlePinch.Rotation;
          break;
      }

      _anchor.localScale = Vector3.one;
    }
}
