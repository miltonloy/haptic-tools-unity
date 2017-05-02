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
    private PinchDetector _pinchDetectorA;
    public PinchDetector PinchDetectorA {
      get {
        return _pinchDetectorA;
      }
      set {
        _pinchDetectorA = value;
      }
    }

    [SerializeField]
    private PinchDetector _pinchDetectorB;
    public PinchDetector PinchDetectorB {
      get {
        return _pinchDetectorB;
      }
      set {
        _pinchDetectorB = value;
      }
    }

    [SerializeField]
    private RotationMethod _oneHandedRotationMethod;

    private Transform _anchor;
    private Rigidbody _rigidbody;
    private bool _isActive = false;

    private float _defaultNearClip;

    void Start() {
      // Acá va el anchor este que no se para que lo usa
      GameObject pinchControl = new GameObject("Grab Anchor");
      _anchor = pinchControl.transform;
      _anchor.transform.parent = transform.parent;
      transform.parent = _anchor;

      _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {

      bool didUpdate = false;
      if(_pinchDetectorA != null)
        didUpdate |= _pinchDetectorA.DidChangeFromLastFrame;
      if(_pinchDetectorB != null)
        didUpdate |= _pinchDetectorB.DidChangeFromLastFrame;

      if (didUpdate) {
        transform.SetParent(null, true);
      }

      if (_pinchDetectorA != null && _pinchDetectorA.IsActive) {
        _isActive = true;
        transformSingleAnchor(_pinchDetectorA);
      } else if (_pinchDetectorB != null && _pinchDetectorB.IsActive) {
        _isActive = true;
        transformSingleAnchor(_pinchDetectorB);
      } else {
        _isActive = false;
      }

      _rigidbody.isKinematic = _isActive;

      if (didUpdate) {
        transform.SetParent(_anchor, true);
      }
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
