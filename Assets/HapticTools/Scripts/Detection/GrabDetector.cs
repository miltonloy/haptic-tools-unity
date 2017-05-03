using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class GrabDetector : MonoBehaviour {

	private ProximityDetector _proximityDetector;
	private PinchDetector _pinchDetector;
	private HandheldObject _current;

	// Use this for initialization
	void Start () {
		_proximityDetector = GetComponent<ProximityDetector>();
		_pinchDetector = GetComponent<PinchDetector>();
	}
	
	public void Activado()
	{
		_current = _proximityDetector.CurrentObject.GetComponent<HandheldObject>();
		_current.PinchEnabled(_pinchDetector);
	}

	public void Desactivado()
	{
		_current.PinchDisabled();
	}
}
