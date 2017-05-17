using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HapticEffectAction : MonoBehaviour
{
	public abstract void StartEffect(GameObject activator = null);
}
