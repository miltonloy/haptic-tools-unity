using UnityEngine;
using System.Collections;
using Leap.Unity;
using UnityEngine.UI;

public class FloatingCube : MonoBehaviour {

    Rigidbody rb;
    Transform t;
    float maxVelocity = 5.0f;
    float force = 0.5f;
    Vector3 initialPosition;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        initialPosition = transform.position;
        // gameObject.SetActive(false);
    }

    public void ToggleFloatingCube(Toggle toggle)
    {
        gameObject.SetActive(toggle.isOn);
        if (toggle.isOn)
        {
            transform.position = initialPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        } else
        {
            StopAllCoroutines();
        }
    }

    // Llamarlo cuando un evento active la atracción del cubo hacia la mano
    public void StartFollowHand (IHandModel handModel)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(FollowHand(handModel));
        }
    }

    // Llamarlo para que la mano deje de atraer el cubo
    public void StopFollowHand ()
    {
        StopAllCoroutines();
    }

    IEnumerator FollowHand (IHandModel hand)
    {
        for (;;)
        {
            Vector3 palmPosition = hand.GetLeapHand().PalmPosition.ToVector3();
            rb.AddForce((palmPosition - t.position) * force, ForceMode.Acceleration);
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
            yield return null;
        }
    }
}
