using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldSpawner : MonoBehaviour {

	public GameObject spawneable;

	public void Spawn ()
	{
		GameObject newSphere = GameObject.Instantiate(spawneable);
	}
}
