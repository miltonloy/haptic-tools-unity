using UnityEngine;
using System.Collections;

public class HapticMaterialDetector : MonoBehaviour {

	void Start () {
        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
            {
                foreach (Transform grandchild in child)
                {
                    AddDetector(grandchild.gameObject);
                }
            }
            else
            {
                AddDetector(child.gameObject);
            }
        }
    }

    private void AddDetector(GameObject gameObject)
    {
        if (gameObject.GetComponent<Actuator>())
        {
            BoneTouchDetector detector = gameObject.AddComponent<BoneTouchDetector>();
            detector.Owner = this;
        }
    }

    public void EffectActivation (string hapticEffectName)
    {
        HapticEffectAction hapticEffect = GetComponent(hapticEffectName) as HapticEffectAction;
        if (hapticEffect)
        {
            hapticEffect.StartEffect();
        }
    }

    /**
     * BoneCollisionDetector es el encargado de detectar las colisiones en cada hueso y lanzar la animación que corresponda
     */
    class BoneTouchDetector : MonoBehaviour
    {
        public HapticMaterialDetector Owner { set { _owner = value; } }

        HapticMaterialDetector _owner;
        Actuator _actuator;
        HapticMaterialDepth _currentMaterialDepth;

        void Start()
        {
            _actuator = GetComponent<Actuator>();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!_owner) return;
            CheckEffectTouch(collision);
            CheckSingleTouch(collision);
            _currentMaterialDepth = collision.gameObject.GetComponent<HapticMaterialDepth>();
            CheckDepthTouch(collision);
        }

        void OnCollisionStay(Collision collision)
        {
            CheckDepthTouch(collision);
        }

        void OnCollisionExit(Collision collision)
        {
            _actuator.Value = 0;
            _currentMaterialDepth = null;
        }

        void CheckEffectTouch(Collision collision)
        {
            HapticEffectTrigger hapticEffect = collision.gameObject.GetComponent<HapticEffectTrigger>();
            if (hapticEffect)
            {
                if (hapticEffect.ScriptName.Length > 0)
                {
                    _owner.EffectActivation(hapticEffect.ScriptName);
                }
            }
        }

        void CheckSingleTouch(Collision collision)
        {
            HapticMaterialSingle materialSingle = collision.gameObject.GetComponent<HapticMaterialSingle>();
            if (materialSingle)
            {
                _owner.SendMessage("ControlRequest", SendMessageOptions.DontRequireReceiver);
                AnimationCurve curve = materialSingle.Curve;
                StopAllCoroutines();
                StartCoroutine(SingleContinuousCurve(curve));
            }
        }

        // Hace un efecto táctil usando una función de profundidad de colisión para materiales
        void CheckDepthTouch(Collision collision)
        {
            if (_currentMaterialDepth)
            {
                _owner.SendMessage("ControlRequest", SendMessageOptions.DontRequireReceiver);
                ContactPoint cp = collision.contacts[collision.contacts.Length - 1];
                AnimationCurve curve = _currentMaterialDepth.Curve;
                float penetrationRatio = Mathf.Clamp((cp.separation - _currentMaterialDepth.MaxDistance) / (_currentMaterialDepth.MinDistance - _currentMaterialDepth.MaxDistance), 0f, 1f);
                _actuator.Value = curve.Evaluate(penetrationRatio);
            }
        }

        // Hace un efecto táctil disparando una curva
        IEnumerator SingleContinuousCurve(AnimationCurve curve)
        {
            Keyframe[] keys = curve.keys;
            if (keys.Length == 0) { yield break; }
            float time = keys[0].time;
            float duration = keys[keys.Length - 1].time;
            while (time < duration)
            {
                _actuator.Value = curve.Evaluate(time);
                time += Time.deltaTime;
                yield return null;
            }
        }

    }

}
