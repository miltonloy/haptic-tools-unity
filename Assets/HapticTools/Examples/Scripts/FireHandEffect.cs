using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandEffect : HapticEffectAction
{
	public GameObject FirePrefab;
	Animator _animator;
	Transform _palm;
	bool _isOnFire = false;
	GameObject _fire;

	void Start ()
	{
		_animator = GetComponent<Animator>();
		_animator.enabled = false;
		_palm = transform.Find("palm");
	}

	// Este método lo llaman los otros scripts para interrumpir
    void ControlRequest ()
    {
		if (_isOnFire)
		{
			StopEffect();
		}
    }

	public override void StartEffect (GameObject activator)
	{
		if (_isOnFire) return;
		_isOnFire = true;
		Destroy(activator);
		// Iniciar efecto háptico
		_animator.enabled = true;
		_animator.SetBool("OnFire", true);
		StartCoroutine(UpdateFire());
		Invoke("StopEffect", 4f);
		// Agregar el fuego
		_fire = Instantiate(FirePrefab, transform);
	}

	void StopEffect ()
	{
		_isOnFire = false;
		// Terminar efecto háptico
		_animator.SetBool("OnFire", false);
		_animator.enabled = false;
		ResetActuators();
		// Quitar fuego
		Destroy(_fire);
	}

	IEnumerator UpdateFire ()
	{
		while (_isOnFire)
		{
			if (_fire)
			{
				_fire.transform.position = _palm.position;
			}
			yield return null;
		}
	}

	void OnDisable()
	{
		ControlRequest();
	}

	void ResetActuators()
	{
		foreach (Actuator actuator in GetComponentsInChildren<Actuator>())
		{
			actuator.Value = 0;
		}
	}
}