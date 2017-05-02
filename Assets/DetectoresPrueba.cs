using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class DetectoresPrueba : MonoBehaviour {

	private ProximityDetector _proximityDetector;
	private GameObject _current;

	// Use this for initialization
	void Start () {
		_proximityDetector = GetComponent<ProximityDetector>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activado()
	{
		_current = _proximityDetector.CurrentObject;
		// TODO avisarle a _current que esta siendo agarrado
		// TODO con este esquema el script "handableObject" pierde la logica actual y queda para recibir la posicion de rotacion y movimiento segun el pinch

	}

	public void Desactivado()
	{
		// TODO avisarle a _current que se acabo
	}
}
