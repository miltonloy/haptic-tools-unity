using UnityEngine;
using System.Collections.Generic;
using Leap.Unity;

public class HandTriggerDetector : Detector
{
    private int _bonesOnTrigger = 0;
    private Animator _animator;
    private List<Actuator> actuators = new List<Actuator>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            // Busco los Colliders no los Actuators
            if (child.GetComponent<Collider>())
            {
                SetUpBone(child.gameObject);
            }
            else
            {
                foreach (Transform grandchild in child)
                {
                    SetUpBone(grandchild.gameObject);
                }
            }
        }
        _animator = GetComponent<Animator>();
        SetEnabled(false);
    }

    void SetUpBone(GameObject bone)
    {
        // Creo el componente para detectar triggers
        var boneDetector = bone.AddComponent<BoneTriggerDetector>();
        boneDetector.Owner = this;
        // Veo de agregar el actuador para al terminar la animacion forzar el reinicio
        Actuator actuator = bone.GetComponent<Actuator>();
        if (actuator != null)
        {
            actuators.Add(actuator);
        }
    }

    void SetEnabled (bool active)
    {
        if (_animator)
        {
            if (active)
            {
                ResetActuators();
            }
            _animator.enabled = active;
        }
    }

    void ResetActuators()
    {
        foreach (Actuator actuator in actuators)
        {
            actuator.Value = 0;
        }
    }

    void ControlRequest ()
    {
        SetEnabled(false);
    }

    public void BoneEnterTrigger()
    {
        _bonesOnTrigger++;
        if (_bonesOnTrigger == 1)
        {
            SetEnabled(true);
            Activate();
        }
    }

    public void BoneExitTrigger()
    {
        _bonesOnTrigger--;
        if (_bonesOnTrigger == 0)
        {
            Deactivate();
        }
    }

    void OnDisable ()
    {
        _bonesOnTrigger = 0;
        Deactivate();
    }

    class BoneTriggerDetector : MonoBehaviour
    {
        public HandTriggerDetector Owner;

        void OnTriggerEnter(Collider collider)
        {
            Owner.BoneEnterTrigger();
        }

        void OnTriggerExit(Collider collier)
        {
            Owner.BoneExitTrigger();
        }
    }
}