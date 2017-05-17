using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandEffect : HapticEffectAction
{
	public GameObject FirePrefab;
	Animator _animator;
	Transform _palm;
	// Coroutine _fireUpdate;
	bool _isOnFire = false;
	GameObject _fire;

	void Start ()
	{
		_animator = GetComponent<Animator>();
		_palm = transform.Find("palm");
	}

	// void OnEnable()
	// {
	// 	// BORRAR
	// 	Invoke("DaleVieja", 2f);
	// }

	// Este método lo llaman los otros scripts para interrumpir
    void ControlRequest ()
    {
		if (_isOnFire)
		{
			StopEffect();
		}
    }

	// void DaleVieja ()
	// {
	// 	StartEffect(new GameObject());
	// }

	public override void StartEffect (GameObject activator)
	{
		if (_isOnFire) return;
		_isOnFire = true;
		Destroy(activator);
		// Iniciar efecto háptico
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
}