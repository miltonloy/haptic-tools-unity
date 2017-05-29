using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExploder : MonoBehaviour {

	void Explode ()
	{
		Destroy(gameObject);
	}
}
